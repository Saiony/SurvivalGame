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

        public IPlayerState State { get; private set; }
        public static PlayerController Instance = null;
        public IPlayerItems Items { get; private set; }

        void Awake()
        {
            if (!Instance)
                Instance = this;
            else
                Destroy(gameObject);

            State = new PlayerIdleState();
            Items = new PlayerItems(new Inventory(), new EquippedItems());
            ObjectPicker.Init(this);
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
                                                (item as WeaponSO).Command, attack, (item as WeaponSO).Slot);
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
                RgdBody.velocity = Vector3.zero;

            //TODO: colocar Speed numa var
            Animator.SetFloat("Speed", RgdBody.velocity.magnitude);
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
                transform.DOLocalRotate(new Vector3(0, 90, 0), RotationSpeed);
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
