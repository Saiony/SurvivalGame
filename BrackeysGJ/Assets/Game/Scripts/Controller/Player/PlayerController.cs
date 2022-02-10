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

namespace Game.Scripts.Controller.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour, IObjectPickerListener, IEquipmentListener
    {
        [SerializeField]
        private float _speed = 0;
        private float Speed => _speed;

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

        public IPlayerState State { get; private set; }
        public static PlayerController Instance = null;
        public IPlayerItems Items { get; private set; }

        private float TurnSmoothTime = 0.1f;

        void Awake()
        {
            if (!Instance)
                Instance = this;
            else
                Destroy(gameObject);

            State = new PlayerIdleState();
            Items = new PlayerItems(new Inventory(), new EquippedItems());
            ObjectPicker.Init(this);
            Items.EquippedItems.Subscribe(this);
            InitialItens.ForEach(item =>
            {
                switch (item)
                {
                    case ToolSO _:
                        var tool = new Tool((item as ToolSO).Id, (item as ToolSO).Name, (item as ToolSO).Description, (item as ToolSO).Image, (item as ToolSO).Command);
                        Items.Inventory.AddItem(tool);
                        break;
                    case ConsumableSO _:
                        var consumable = new Consumable((item as ConsumableSO).Id, (item as ConsumableSO).Name, (item as ConsumableSO).Description, (item as ConsumableSO).Image, (item as ConsumableSO).Command);
                        Items.Inventory.AddItem(consumable);
                        break;
                    case MiscSO _:
                        var misc = new Misc((item as MiscSO).Id, (item as MiscSO).Name, (item as MiscSO).Description, (item as MiscSO).Image);
                        Items.Inventory.AddItem(misc);
                        break;
                    case WeaponSO _:
                        var attack = new Attack((item as WeaponSO).DamagesType, (item as WeaponSO).DamagesValue);
                        var weapon = new Weapon((item as WeaponSO).Id, (item as WeaponSO).Name, (item as WeaponSO).Description, (item as WeaponSO).Image,
                                                (item as WeaponSO).Command, attack, (item as WeaponSO).Slot, (item as WeaponSO).Prefab);
                        Items.Inventory.AddItem(weapon);
                        break;
                    default:
                        throw new InvalidOperationException("Invalid item type");
                }
            });
        }

        private void Update()
        {
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

            CharacterController.Move(moveDir * Speed * UnityEngine.Time.deltaTime);

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
    }

    public enum Direction
    {
        Unknown,
        Left,
        Right,
        Up,
        Down
    }
}
