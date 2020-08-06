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

        public IEnumerator GetItem(Item.Item item)
        {
            yield return AnimateGetItem(item);

            item.gameObject.transform.position = Instance.hand.transform.position;
            item.gameObject.transform.parent = Instance.hand.transform;
            this.ItemHeld = item;
        }

        private IEnumerator AnimateGetItem(Item.Item item)
        {
            yield return null;
        }

        public static Dialogue Dialog(params string[] sentences)
        {
            return null;
        }

        void Start()
        {
            rgdBody = GetComponent<Rigidbody>();
            InputBlocked = false;
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
        }

        public void DisableInput()
        {
            InputBlocked = true;
        }

        public void EnableInput()
        {
            InputBlocked = false;
        }

        public void ExchangeItem(Item.Item item)
        {
            if (!item.Equals(ItemHeld))
            {
                ItemHeld.transform.position = item.transform.position;
                ItemHeld.transform.parent = null;
                StartCoroutine(GetItem(item));
            }
            else
            {
                ItemHeld.transform.parent = null;
            }
        }
    }
}
