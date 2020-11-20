using System;
using System.Collections;
using DG.Tweening;
using Game.Scripts.Controller.Interact;
using Game.Scripts.Controller.Interface.Item;
using Game.Scripts.Controller.Player;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.Controller.Item
{
    public class ItemController : Interactable, ITimeChangeable
    {
        [SerializeField]
        private InteractableItemSO _interactableItemSO = null;
        private InteractableItemSO InteractableItemSO => _interactableItemSO;

        [SerializeField]
        private Collider _collider = null;
        private Collider Collider => _collider;

        [SerializeField]
        private Transform _feet = null;
        public Transform Feet => _feet;
        public string Id => InteractableItemSO.name;

        public GameObject PastObject => InteractableItemSO.PastObject;
        public GameObject FutureObject => InteractableItemSO.FutureObject;

        public bool Fowardable => InteractableItemSO.FutureObject != null;
        public bool Rewindable => InteractableItemSO.PastObject != null;

        public ItemController FowardTime()
        {
            if (Fowardable)
            {
                var item = Foward();
                return item;
            }
            throw new ArgumentOutOfRangeException(nameof(InteractableItemSO.FutureObject));
        }

        public ItemController RewindTime()
        {
            if (Rewindable)
            {
                var item = Rewind();
                return item;
            }
            throw new ArgumentOutOfRangeException(nameof(InteractableItemSO.PastObject));
        }

        protected override void OnPlayerInteract()
        {
            Debug.Log("Player interacted with Item");
            if (PlayerController.Instance.HasItem)
            {
                PlayerController.Instance.ExchangeItem(this);
                Collider.enabled = true;
            }
            else
            {
                PlayerController.Instance.SetItem(this);
                Collider.enabled = false;
            }
        }

        public void OnItemThrown()
        {
            Collider.enabled = true;
        }

        private ItemController Foward()
        {
            var currentObj = this.gameObject;
            var newObj = GameObject.Instantiate(
                InteractableItemSO.FutureObject,
                new Vector3(currentObj.transform.position.x, currentObj.transform.position.y, currentObj.transform.position.z),
                new Quaternion(currentObj.transform.rotation.x, currentObj.transform.rotation.y, currentObj.transform.rotation.z, currentObj.transform.rotation.w));
            StartCoroutine(RunAnimation(currentObj, newObj));
            return newObj.GetComponent<ItemController>();
        }

        private ItemController Rewind()
        {
            var currentObj = this.gameObject;
            var newObj = InteractableItemSO.PastObject;
            StartCoroutine(RunAnimation(currentObj, newObj));
            return newObj.GetComponent<ItemController>();
        }

        private IEnumerator RunAnimation(GameObject objToDestroy, GameObject objToInstantiate)
        {
            yield return Shrink(objToDestroy);
            Destroy(objToDestroy);

            yield return Shrink(objToInstantiate, false);
            yield return Expand(objToInstantiate);
        }

        public void DestroyItself()
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(Shrink(gameObject, true));
            seq.AppendCallback(() => Destroy(gameObject));
        }

        private Sequence Shrink(GameObject go, bool animate = true)
        {
            Sequence seq = DOTween.Sequence();
            // bool animate diz se o Shrink vai ter um tempo pra diminuir
            // Todo: @Mike

            return seq;
        }

        private IEnumerator Expand(GameObject go, bool animate = true)
        {
            yield return null;
            // Todo: @Mike
        }

        public bool Equals(ItemController other)
        {
            return InteractableItemSO.name == other.InteractableItemSO.name;
        }

        protected override void OnPlayerEnter()
        {
        }

        protected override void OnPlayerExit()
        {
        }
    }
}