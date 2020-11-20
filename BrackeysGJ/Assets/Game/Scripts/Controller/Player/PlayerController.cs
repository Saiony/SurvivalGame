using DG.Tweening;
using System.Collections;
using UnityEngine;
using Game.Scripts.Controller.Dialog;
using System;
using Game.Scripts.Controller.Interact;
using Game.Scripts.Controller.Item;
using System.Linq;

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
            var results = Physics.OverlapBox(ContactArea.transform.position, ContactArea.bounds.size, Quaternion.identity);
            var interactableResults = results.ToList().Where(x => x.GetComponent<Interactable>());
            foreach (var interactableResult in interactableResults)
            {
                interactableResult.GetComponent<Interactable>().Interact();
                return;
            }

            if (HasItem)
            {
                ThrowItem(ItemHeld);
            }
        }

        public void SetItem(ItemController item)
        {
            Debug.Log("SetItem: " + item.name);
            item.gameObject.transform.position = Instance.Hand.transform.position +
                                                 new Vector3(0, item.gameObject.GetComponent<MeshRenderer>().bounds.size.y / 2, 0);
            item.gameObject.transform.parent = Instance.Hand.transform;
            ItemHeld = item;
        }

        public void ExchangeItem(ItemController item)
        {
            if (item.Equals(ItemHeld))
                throw new InvalidOperationException("Can't exchange an item for itself");

            ItemHeld.transform.position = item.transform.position;
            ItemHeld.transform.parent = null;
            SetItem(item);
        }

        private void ThrowItem(ItemController item)
        {
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
