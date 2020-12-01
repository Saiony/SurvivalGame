using DG.Tweening;
using System.Collections;
using UnityEngine;
using Game.Scripts.Controller.Dialog;
using System;
using Game.Scripts.Controller.Interact;
using Game.Scripts.Controller.Item;
using System.Linq;
using System.Collections.Generic;

namespace Game.Scripts.Controller.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
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

        public Item.ItemController ItemHeld { get; private set; }
        public bool HasItem => ItemHeld != null;
        private Rigidbody RgdBody { get; set; }

        public static PlayerController Instance = null;

        void Awake()
        {
            if (!Instance)
                Instance = this;
            else
                Destroy(gameObject);
        }

        void Start()
        {
            RgdBody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            var command = InputHandler.Instance.HandleInput();

            if (command != null)
                command.Execute(this);
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

        public void Interact()
        {
            Debug.Log("Cmd Interact");

            var interactables = GetInteractablesOnRange();
            //TODO: criar um IsNullEmpty
            if (HasItem && !interactables.Any())
            {
                ThrowItem(ItemHeld);
                return;
            }

            interactables.FirstOrDefault().Interact(transform.position + transform.forward);
        }

        private List<Interactable> GetInteractablesOnRange()
        {
            var results = Physics.OverlapBox(ContactArea.transform.position, ContactArea.bounds.size, Quaternion.identity);
            var interactableList = new List<Interactable>();
            results.ToList().ForEach(x =>
            {
                var interactable = x.GetComponent<Interactable>();
                if (interactable != null)
                    interactableList.Add(interactable);
            });
            return interactableList;
        }

        public void Plow()
        {
            //TODO: regras de negócio de Plow
            var interactables = GetInteractablesOnRange();
            if (interactables.Count > 0)
                interactables.First().Plow(transform.position + transform.forward);
        }

        public void Water()
        {
            //TODO: regras de negócio de Water
            var interactables = GetInteractablesOnRange();
            if (interactables.Count > 0)
                interactables.First().Water(transform.position + transform.forward);
        }

        public void Plant()
        {
            //TODO: regras de negócio de Plant
            var interactables = GetInteractablesOnRange();
            if (interactables.Count > 0)
                interactables.First().Plant(transform.position + transform.forward);
        }

        public void SetItem(ItemController item)
        {
            Debug.Log("SetItem: " + item.name);
            item.gameObject.transform.position = Instance.Hand.transform.position +
                                                 new Vector3(0, item.gameObject.GetComponent<MeshRenderer>().bounds.size.y / 2, 0);
            item.gameObject.transform.parent = Instance.Hand.transform;
            ItemHeld = item;
        }

        private void ThrowItem(ItemController item)
        {
            Debug.Log("ThrowItem: " + item.name);
            ItemHeld.transform.parent = null;
            ItemHeld.transform.DOMove(
                new Vector3
                (
                    PlaceObjectPosition.position.x,
                    PlaceObjectPosition.position.y + (item.transform.position.y - item.Feet.position.y),
                    PlaceObjectPosition.position.z
                ), 0.5f).OnComplete(() =>
                {
                    item.OnItemThrown();
                    ItemHeld = null;
                });
            //To-do: @mike animação

            var playerloop = UnityEngine.LowLevel.PlayerLoop.GetDefaultPlayerLoop();
        }

        public void GiveItemHeld()
        {
            ItemHeld.DestroyItself();
            ItemHeld = null;
        }

        public void TimeTravel()
        {
            Debug.Log("TIME TRAVEL");
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
