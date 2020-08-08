using DG.Tweening;
using System.Collections;
using UnityEngine;
using Game.Scripts.Controller.Dialog;

namespace Game.Scripts.Controller.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float Speed;

        [SerializeField]
        private float RotationSpeed;

        [SerializeField]
        private Animator Animator;

        private Rigidbody rgdBody = null;

        public bool InputBlocked { get; private set; }

        public Item.Item ItemHeld { get; private set; }

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
            InputBlocked = false;
        }

        public static Dialogue Dialog(params string[] sentences)
        {
            return null;
        }

        void FixedUpdate()
        {
            Movement();
        }

        private void Movement()
        {
            rgdBody.velocity = Vector3.zero;

            if (InputBlocked)
                return;

            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var direction = Vector3.zero;

            if (horizontal > 0) //Right
            {
                direction = Vector3.right * horizontal;
                gameObject.transform.DOLocalRotate(new Vector3(0, 90, 0), RotationSpeed);
            }
            else if (horizontal < 0) //Left
            {
                direction = Vector3.left * -horizontal;
                gameObject.transform.DOLocalRotate(new Vector3(0, -90, 0), RotationSpeed);
            }
            else if (vertical > 0) //Up
            {
                direction = Vector3.forward * vertical;
                gameObject.transform.DOLocalRotate(new Vector3(0, 0, 0), RotationSpeed);
            }
            else if (vertical < 0) //Down
            {
                direction = Vector3.back * -vertical;
                gameObject.transform.DOLocalRotate(new Vector3(0, 180, 0), RotationSpeed);
            }

            rgdBody.velocity = direction * Speed;
            Animator.SetFloat("Speed", rgdBody.velocity.magnitude);
            Debug.Log("speed: " +rgdBody.velocity.magnitude);
        }

        public void DisableInput()
        {
            InputBlocked = true;
        }

        public void EnableInput()
        {
            InputBlocked = false;
        }

        public void GetItem(Item.Item item)
        {
            item.gameObject.transform.position = Instance.hand.transform.position + 
                                                 new Vector3(0, item.gameObject.GetComponent<MeshRenderer>().bounds.size.y/2, 0);
            item.gameObject.transform.parent = Instance.hand.transform;
            ItemHeld = item;
        }

        public void ExchangeItem(Item.Item item)
        {
            if (!item.Equals(ItemHeld))
            {
                ItemHeld.transform.position = item.transform.position;
                ItemHeld.transform.parent = null;
                GetItem(item);
            }
            else
            {
                ThrowItem(item);
            }
        }

        private void ThrowItem(Item.Item item)
        {
            ItemHeld.transform.parent = null;
            //To-do: @mike animação
        }

        public void GiveItemHeld()
        {
            ItemHeld.DestroyItself();
            ItemHeld = null;
        }
    }
}
