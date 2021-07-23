using DG.Tweening;
using System.Collections;
using UnityEngine;
using Game.Scripts.Controller.Dialog;
using System;
using Game.Scripts.Controller.Itens;
using System.Linq;
using System.Collections.Generic;
using Game.Scripts.ScriptableObjects;

namespace Game.Scripts.Controller.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour, IObjectPickerListener
    {
        [SerializeField]
        private float _speed = 0;
        private float Speed => _speed;

        [SerializeField]
        private float _rotationSpeed = 0;
        private float RotationSpeed => _rotationSpeed;

        [SerializeField]
        private Animator _animator = null;
        private Animator Animator => _animator;

        [SerializeField]
        private Collider _contactArea = null;
        private Collider ContactArea => _contactArea;

        [SerializeField]
        private Transform _placeObjectPosition = null;
        private Transform PlaceObjectPosition => _placeObjectPosition;

        [SerializeField]
        private GameObject _hand = null;
        public GameObject Hand => _hand;

        [SerializeField]
        private PlayerAnimatorController _playerAnimator = null;
        private PlayerAnimatorController PlayerAnimator => _playerAnimator;

        [SerializeField]
        private InventoryController _inventory = null;
        private InventoryController Inventory => _inventory;

        [SerializeField]
        private GameObject _itemOnHand = null;
        private GameObject ItemOnHand => _itemOnHand;

        [SerializeField]
        private ObjectPickerController _objectPicker = null;
        private ObjectPickerController ObjectPicker => _objectPicker;

        [SerializeField]
        private List<ItemSO> _initialItens = null;
        private List<ItemSO> InitialItens => _initialItens;

        [SerializeField]
        private HandController _handController = null;
        public HandController HandController => _handController;

        public Itens.ItemController ItemHeld { get; private set; }
        public bool HasItem => ItemHeld != null;
        private Rigidbody RgdBody { get; set; }

        public static PlayerController Instance = null;

        void Awake()
        {
            if (!Instance)
                Instance = this;
            else
                Destroy(gameObject);

            ObjectPicker.Init(this);
            InitialItens.ForEach(item =>
            {
                switch (item)
                {
                    case ToolSO _:
                        var tool = new Tool((item as ToolSO).Id, (item as ToolSO).name, (item as ToolSO).Description, (item as ToolSO).Image, (item as ToolSO).Command);
                        Inventory.AddItem(tool);
                        break;
                    case ConsumableSO _:
                        var consumable = new Consumable((item as ConsumableSO).Id, (item as ConsumableSO).name, (item as ConsumableSO).Description, (item as ConsumableSO).Image, (item as ConsumableSO).Command);
                        Inventory.AddItem(consumable);
                        break;
                    case MiscSO _:
                        var misc = new Misc((item as MiscSO).Id, (item as MiscSO).name, (item as MiscSO).Description, (item as MiscSO).Image);
                        Inventory.AddItem(misc);
                        break;
                    case WeaponSO _:
                        var attack = new Attack((item as WeaponSO).DamagesType, (item as WeaponSO).DamagesValue);
                        var weapon = new Weapon((item as WeaponSO).Id, (item as WeaponSO).name, (item as WeaponSO).Description, (item as WeaponSO).Image,
                                                (item as WeaponSO).Command, attack);
                        Inventory.AddItem(weapon);
                        break;
                    default:
                        throw new InvalidOperationException("Invalid item type");
                }
            });
        }

        void Start()
        {
            RgdBody = GetComponent<Rigidbody>();
            HandController.Init(Inventory);
        }

        private void Update()
        {
            var commands = InputHandler.Instance.HandleInput();

            if (commands.Count > 0)
                commands.ForEach(x => x.Execute());
            else
                RgdBody.velocity = Vector3.zero;

            Animator.SetFloat("Speed", RgdBody.velocity.magnitude);
        }

        public static Dialogue Dialog(params string[] sentences)
        {
            return null;
        }

        #region Movement
        public void Move_Left()
        {
            Move(Direction.Left);
        }

        public void Move_Right()
        {
            Move(Direction.Right);
        }

        public void Move_Up()
        {
            Move(Direction.Up);
        }

        public void Move_Down()
        {
            Move(Direction.Down);
        }

        private void Move(Direction direction)
        {
            RgdBody.velocity = Vector3.zero;
            Vector3 dir = new Vector3();

            if (direction == Direction.Right) //Right
            {
                dir = Vector3.right;
                gameObject.transform.DOLocalRotate(new Vector3(0, 90, 0), RotationSpeed);
            }
            else if (direction == Direction.Left) //Left
            {
                dir = Vector3.left;
                gameObject.transform.DOLocalRotate(new Vector3(0, -90, 0), RotationSpeed);
            }
            else if (direction == Direction.Up) //Up
            {
                dir = Vector3.forward;
                gameObject.transform.DOLocalRotate(new Vector3(0, 0, 0), RotationSpeed);
            }
            else if (direction == Direction.Down) //Down
            {
                dir = Vector3.back;
                gameObject.transform.DOLocalRotate(new Vector3(0, 180, 0), RotationSpeed);
            }
            else
                throw new Exception($"Movement direction {direction}");

            RgdBody.velocity = dir * Speed;
        }
        #endregion Movement

        private List<T> GetInteractablesOnRange<T>() where T : IBaseInteractable
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

        public void PlayAttackAnimation(Attack attack)
        {
            InputHandler.Instance.DisableInput();
            Animator.SetTrigger("Attacking_Trigger");

            var attackAnim = Animator.runtimeAnimatorController.animationClips.FirstOrDefault(x => x.name == "Attacking");
            var actionTime = attackAnim.events.FirstOrDefault(x => x.functionName == "Attack").time;

            StartCoroutine(DoTheActualAttackThing(actionTime, attack));
        }

        public IEnumerator DoTheActualAttackThing(float time, Attack attack)
        {
            yield return new WaitForSeconds(time);

            //TODO: regras de negócio de Chop
            ContactArea.enabled = true;
            var interactables = GetInteractablesOnRange<IDamageable>();
            if (interactables == null)
                yield return null;

            interactables.ForEach(x => x.ReceiveAttack(attack));
        }

        public void SetItem(ItemController item)
        {
            Debug.Log("SetItem: " + item.name);
            item.gameObject.transform.position = Instance.Hand.transform.position +
                                                 new Vector3(0, item.gameObject.GetComponent<MeshRenderer>().bounds.size.y / 2, 0);
            item.gameObject.transform.parent = Instance.Hand.transform;
            ItemHeld = item;
        }

        public void SelectQuickItem(int index)
        {
            Inventory.SelectQuickItem(index);
        }

        public void GiveItem(Item item)
        {
            Inventory.AddItem(item);
        }

        public void GiveItemHeld()
        {
            ItemHeld.DestroyItself();
            ItemHeld = null;
        }

        public void UseSelectedItem()
        {
            Inventory.UseSelectedItem();
        }

        public void OnObjectPicked(Item item)
        {
            GiveItem(item);
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
