using DG.Tweening;
using System.Collections;
using UnityEngine;
using Game.Scripts.Controller.Dialog;
using System;
using Game.Scripts.Controller.Itens;
using System.Linq;
using System.Collections.Generic;
using Game.Scripts.ScriptableObjects;
using BrackeysGJ.Assets.Game.Scripts.Controller.Player;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Items;
using BrackeysGJ.Assets.Game.Scripts.Domain.PlayerItems;
using BrackeysGJ.Assets.Game.Scripts.Domain.Items;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Player;
using BrackeysGJ.Assets.Game.Scripts.Manager;
using BrackeysGJ.Assets.Game.Scripts.Manager.Interface;
using BrackeysGJ.Assets.Game.Scripts.Domain.Message;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Message;
using BrackeysGJ.Assets.Game.Scripts.Domain.Player;
using UnityEngine.SceneManagement;
using BrackeysGJ.Assets.Game.Scripts.ScriptableObjects.Player;

namespace Game.Scripts.Controller.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour, IObjectPickerListener, IEquipmentListener, IDamageable
    {
        [SerializeField]
        private float _rotationSpeed = 0;
        private float RotationSpeed => _rotationSpeed;

        [SerializeField]
        private Animator _animator = null;
        public Animator Animator => _animator;

        [SerializeField]
        private Collider _contactArea = null;
        private Collider ContactArea => _contactArea;

        [SerializeField]
        private PlayerAnimatorController _playerAnimator = null;
        private PlayerAnimatorController PlayerAnimator => _playerAnimator;

        [SerializeField]
        private ObjectPickerController _objectPicker = null;
        private ObjectPickerController ObjectPicker => _objectPicker;

        [SerializeField]
        private List<ItemSO> _initialItens = null;
        private List<ItemSO> InitialItens => _initialItens;

        [SerializeField]
        private HandController _handController = null;
        public HandController HandController => _handController;

        [SerializeField]
        private Rigidbody _rgdBody = null;
        private Rigidbody RgdBody => _rgdBody;

        [SerializeField]
        private CharacterController CharacterController;

        [SerializeField]
        private Transform CameraTransform;

        [SerializeField]
        private Transform RespawnPoint;

        [SerializeField]
        private PlayerConfigSO PlayerConfig;

        public IPlayerState State { get; private set; }
        public static PlayerController Instance = null;
        public IPlayerItems Items { get; private set; }
        public IPlayerStats Stats { get; private set; }
        public Collider DetectionCollider => throw new NotImplementedException();

        private float TurnSmoothTime = 0.1f;
        private IMessageManager MessageManager { get; set; }
        private Coroutine RunCoroutine;

        void Awake()
        {
            if (!Instance)
                Instance = this;
            else
                Destroy(gameObject);

            Stats = new PlayerStats(new Hp(12, 12), new Stamina(30, 30), new FoodLevel(50, 50, PlayerConfig.FoodLevel), new Speed(4));
            MessageManager = ManagerProvider.Instance.Get<IMessageManager>();
        }

        private void Start() 
        {
            State = new PlayerIdleState();
            SetInitialItems();    
            StartCoroutine(GainStaminaPerSec());
            StartCoroutine(DecreaseFoodLevelPerSec());

            MessageManager.Broadcast<IHpMessage>(new HpMessage(Stats.Hp));
            MessageManager.Broadcast<IStaminaMessage>(new StaminaMessage(Stats.Stamina));
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.P))
               DebugReceiveAttack();

            var commands = InputHandler.Instance.HandleInput();

            if (commands.Count > 0)
                commands.ForEach(x => x.Execute());
            else
                Animator.SetFloat("Speed", 0f);
        }

        public void UseRightArmItem()
        {
            var item = Items.EquippedItems.Equipments[EquipmentSlot.Right_Hand];
            if(item == null)
            {
                Debug.Log("No item equipped");
                return;
            }
            item.Use();
        }

        public void Attack(Attack attack)
        {
            State = new PlayerAttackState();
            (State as PlayerAttackState).BeginAttack(this, attack, HandController);
        }

        public static Dialogue Dialog(params string[] sentences)
        {
            return null;
        }

        public void Move()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            var direction = new Vector3(horizontal, 0, vertical).normalized;

            var targetAngle  = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + CameraTransform.eulerAngles.y;
            var smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref TurnSmoothTime, 0.1f);
            transform.rotation = Quaternion.Euler(0, smoothedAngle, 0);
            var moveDir = (Quaternion.Euler(0, targetAngle, 0) * Vector3.forward).normalized;

            CharacterController.Move(moveDir * Stats.Speed.Value * UnityEngine.Time.deltaTime);

            Animator.SetFloat("Speed", direction.magnitude);
        }

        public List<T> GetInteractablesOnRange<T>() where T : IBaseInteractable
        {
            var results = Physics.OverlapBox(ContactArea.transform.position, ContactArea.bounds.size, Quaternion.identity);
            var interactableList = new List<T>();
            results.ToList().ForEach(x =>
            {
                var interactable = x.GetComponent<T>();
                if (interactable != null)
                    interactableList.Add(interactable);
            });
            return interactableList;
        }

        public void Interact()
        {
            var interactable = GetInteractablesOnRange<IInteractable>().FirstOrDefault();
            //TODO: criar um IsNullOrEmpty
            if (interactable == null)
                return;

            interactable.OnInteract();
        }

        public void PlayPlowAnimation()
        {
            InputHandler.Instance.DisableInput();
            Animator.SetTrigger("Plowing_Trigger");
        }

        public void DoTheActualPlowThing()
        {
            //TODO: regras de negócio de Plow
            var interactable = GetInteractablesOnRange<IPlowable>().FirstOrDefault(); ;
            if (interactable == null)
                return;
            interactable.OnPlow(transform.position + transform.forward);
        }

        public void PlayWaterAnimation()
        {
            InputHandler.Instance.DisableInput();
            Animator.SetTrigger("Watering_Trigger");
        }

        public void DoTheActualWaterThing()
        {
            //TODO: regras de negócio de Water
            var interactable = GetInteractablesOnRange<IWaterable>().FirstOrDefault();

            if (interactable == null)
                return;
            interactable.OnWater(transform.position + transform.forward);
        }

        public void PlayPlantAnimation()
        {
            InputHandler.Instance.DisableInput();
            Animator.SetTrigger("Planting_Trigger");
        }

        public void DoTheActualPlantThing()
        {
            //TODO: regras de negócio de Plant
            var interactable = GetInteractablesOnRange<IPlantable>().FirstOrDefault();

            if (interactable == null)
                return;
            interactable.OnPlant(transform.position + transform.forward);
        }

        public void OnObjectPicked(Item item)
        {
            Items.Inventory.AddItem(item);
        }

        public void UseQuickItem(int index)
        {
            Items.Inventory.UseQuickItem(index);
        }

        public void OnEquipmentChanged(Dictionary<EquipmentSlot, IEquipment> PlayerEquips)
        {
            var weapon = PlayerEquips[EquipmentSlot.Right_Hand];
            HandController.EquipItem(weapon);
        }

        private void SetInitialItems()
        {
            Items = new PlayerItems(new Inventory(), new EquippedItems());
            ObjectPicker.Init(this);
            Items.EquippedItems.Subscribe(this);
            InitialItens.ForEach(item =>
            {
                switch (item)
                {
                    case ToolSO t:
                        var tool = new Tool(t.Id, t.Name, t.Description, t.Image, t.Command);
                        Items.Inventory.AddItem(tool);
                        break;
                    case ConsumableSO c:
                        var consumable = new Consumable(c.Id, c.Name, c.Description, c.Image, c.Command, c.HungerSatisfied, c.HealthGiven);
                        Items.Inventory.AddItem(consumable);
                        break;
                    case MiscSO m:
                        var misc = new Misc(m.Id, m.Name, m.Description, m.Image);
                        Items.Inventory.AddItem(misc);
                        break;
                    case WeaponSO w:
                        var attack = new Attack(w.DamagesType, w.DamagesValue);
                        var weapon = new Weapon(w.Id, w.Name, w.Description, w.Image,
                                                w.Command, attack, w.Slot, w.Prefab);
                        Items.Inventory.AddItem(weapon);
                        break;
                    default:
                        throw new InvalidOperationException("Invalid item type");
                }
            });
        }

        public void ReceiveAttack(Attack attack)
        {
            if (Stats.Dead)
                return;
            
            foreach (var damage in attack.Damages)
            {
                Stats.Hp.Decrease(damage.Value);
                MessageManager.Broadcast<IHpMessage>(new HpMessage(Stats.Hp));
            }

            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOPunchScale(Vector3.one * 0.05f, 0.3f, 7, 5));
            seq.Play();

            if(Stats.Hp.Current <= 0 && !Stats.Dead)
            {
                Stats.Dead = true;
                SceneManager.LoadScene("YouDied", LoadSceneMode.Additive);
            }
        }

        public void DebugReceiveAttack()
        {
            Stats.Hp.Decrease(2);
            MessageManager.Broadcast<IHpMessage>(new HpMessage(Stats.Hp));

            if(Stats.Hp.Current <= 0 && !Stats.Dead)
            {
                Stats.Dead = true;
                SceneManager.LoadScene("YouDied", LoadSceneMode.Additive);
            }
        }

        public void Respawn()
        {
            Stats = new PlayerStats(new Hp(12, 12), new Stamina(30, 30), new FoodLevel(50, 50, PlayerConfig.FoodLevel), new Speed(4));
            MessageManager.Broadcast<IHpMessage>(new HpMessage(Stats.Hp));
            MessageManager.Broadcast<IStaminaMessage>(new StaminaMessage(Stats.Stamina));

            transform.position = RespawnPoint.position;
            transform.rotation = RespawnPoint.rotation;
        }   

        #region Run
        public void StartRunning()
        {
            Stats.Running = true;
            Stats.Speed.Increase(4);
            RunCoroutine = StartCoroutine(Run());
        }

        public void StopRunning()
        {
            Stats.Running = false;
            Stats.Speed.Decrease(4);
            StopCoroutine(RunCoroutine);
            RunCoroutine = null;
        }

        private IEnumerator Run()
        {
            var waitTime = new WaitForSeconds(0.25f);
            do
            {
                yield return waitTime;
                Debug.Log("Decreasing Stamina: " +Stats.Stamina.Current);
                Stats.Stamina.DecreaseCurrent(1);
                MessageManager.Broadcast<IStaminaMessage>(new StaminaMessage(Stats.Stamina));
            } while (Stats.Stamina.Current > 0);

            StopRunning();
        }
        #endregion

        private IEnumerator GainStaminaPerSec()
        {
            var waitTime = new WaitForSeconds(1f);
            do
            {
                yield return waitTime;
                if(Stats.Running)
                   continue;

                Stats.Stamina.IncreaseCurrent(1);
                MessageManager.Broadcast<IStaminaMessage>(new StaminaMessage(Stats.Stamina));
            } while (true);
        }

        #region FoodLevel

        public void Eat(IConsumable consumable)
        {
            Stats.Hp.Increase(consumable.HealthGiven);
            Stats.FoodLevel.Increase(consumable.HungerSatisfied);

            MessageManager.Broadcast<IHpMessage>(new HpMessage(Stats.Hp));
            MessageManager.Broadcast<IFoodLevelMessage>(new FoodLevelMessage(Stats.FoodLevel));
            
            Debug.Log($"Eating: {consumable.Name} \nHealthGiven: {consumable.HealthGiven} \nHungerSatisfied: {consumable.HungerSatisfied}");
        }

        private IEnumerator DecreaseFoodLevelPerSec()
        {
            var waitTime = new WaitForSeconds(3);
            do
            {
                yield return waitTime;

                var previousState = Stats.FoodLevel.Status;
                Stats.FoodLevel.Decrease(5);
                ApplyFoodLevelStatus(previousState);

                MessageManager.Broadcast<IFoodLevelMessage>(new FoodLevelMessage(Stats.FoodLevel));
            } while (true);
        }
int i = 0;
        private void ApplyFoodLevelStatus(HungerStatus previousState)
        {
            Debug.Log("Hunger Status: " +Stats.FoodLevel.Status.ToString() +" - " +Stats.FoodLevel.Current);
            switch (Stats.FoodLevel.Status)
            {
                case HungerStatus.Satisfied:
                    ApplySatisfiedEffects();
                    break;
                case HungerStatus.Normal:
                    break;
                case HungerStatus.Hungry:
                    ApplyHungerEffects(previousState);
                    break;
                case HungerStatus.Starving:
                    ApplyStarvingEffects(previousState);
                    break;
                default:
                    throw new InvalidCastException("Invalid FoodLevel status: " +Stats.FoodLevel.Status.ToString());
            }
        }

        private void ApplySatisfiedEffects()
        {
            Stats.Hp.Increase(Stats.FoodLevel.Config.HpRestoredPerTick);   
            MessageManager.Broadcast<IHpMessage>(new HpMessage(Stats.Hp));       
        }

        private void ApplyHungerEffects(HungerStatus previousState)
        {
            if(previousState == HungerStatus.Hungry || previousState == HungerStatus.Starving)
                return;

            Stats.Speed.Decrease(Stats.FoodLevel.Config.MovSpeedDebuff);
            Stats.Stamina.DecreaseMax(Stats.FoodLevel.Config.StaminaDebuff);
            MessageManager.Broadcast<IStaminaMessage>(new StaminaMessage(Stats.Stamina));
        }

        private void ApplyStarvingEffects(HungerStatus previousState)
        {
            ApplyHungerEffects(previousState);
            Stats.Hp.Decrease(Stats.FoodLevel.Config.HpDecreasedPerTick);
            MessageManager.Broadcast<IHpMessage>(new HpMessage(Stats.Hp));
        }

        #endregion FoodLevel
    }
}
