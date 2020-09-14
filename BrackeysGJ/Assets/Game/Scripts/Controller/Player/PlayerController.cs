using DG.Tweening;
using System.Collections;
using UnityEngine;
using Game.Scripts.Controller.Dialog;
using System;
using Game.Scripts.Controller.Interact;
using Game.Scripts.Controller.Item;

namespace Game.Scripts.Controller.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float Speed;

        [SerializeField]
        private float RotationSpeed = 0;

        [SerializeField]
        private Animator Animator = null;

        [SerializeField]
        private Collider ContactArea = null;

        private Rigidbody rgdBody = null;

        public Item.ItemController ItemHeld { get; private set; }

        public bool HasItem => ItemHeld != null;

        public GameObject hand;

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
            rgdBody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            var command = InputHandler.Instance.HandleInput();

            if (command != null)
                command.Execute(this);
            else
                rgdBody.velocity = Vector3.zero;
                
            Animator.SetFloat("Speed", rgdBody.velocity.magnitude);
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
            rgdBody.velocity = Vector3.zero;
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

            rgdBody.velocity = dir * Speed;
        }
#endregion Movement

        public void Interact()
        {
            Debug.Log("Cmd Interact");
            var results = Physics.OverlapBox(ContactArea.transform.position, ContactArea.bounds.size, Quaternion.identity);
            foreach (var result in results)
            {
                if(!result.isTrigger && result.GetComponent<Interactable>())
                {
                    result.GetComponent<Interactable>().Interact();
                    return;
                }
            }
        }

        public void SetItem(ItemController item)
        {
            item.gameObject.transform.position = Instance.hand.transform.position +
                                                 new Vector3(0, item.gameObject.GetComponent<MeshRenderer>().bounds.size.y / 2, 0);
            item.gameObject.transform.parent = Instance.hand.transform;
            ItemHeld = item;
        }

        public void ExchangeItem(ItemController item)
        {
            if (!item.Equals(ItemHeld))
            {
                ItemHeld.transform.position = item.transform.position;
                ItemHeld.transform.parent = null;
                SetItem(item);
            }
            else
            {
                ThrowItem(item);
            }
        }

        private void ThrowItem(ItemController item)
        {
            ItemHeld.transform.parent = null;
            //To-do: @mike animação
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
