using System;
using System.Collections;
using DG.Tweening;
using Game.ScriptableObjects;
using Game.Scripts.Controller.Interact;
using Game.Scripts.Controller.Interface.Item;
using Game.Scripts.Controller.Player;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.Controller.Item
{
    public abstract class Item : Interactable, ITimeChangeable
    {
        public bool Fowardable => InteractableItemSO.FutureObject != null;
        public bool Rewindable => InteractableItemSO.PastObject != null;
        public InteractableItemSO InteractableItemSO;
        public Rigidbody rigidbody;

        void Start()
        {
            base.Start();
            rigidbody = this.GetComponent<Rigidbody>();
        }

        public void FowardTime()
        {
            if (Fowardable)
            {
                Foward();
                OnFoward();
                return;
            }
            throw new ArgumentOutOfRangeException(nameof(InteractableItemSO.FutureObject));
        }

        public void RewindTime()
        {
            if (Rewindable)
            {
                Rewind();
                OnRewind();
                return;
            }
            throw new ArgumentOutOfRangeException(nameof(InteractableItemSO.PastObject));
        }

        protected override void OnPlayerInteract()
        {
            if (PlayerController.Instance.HasItem)
            {
                PlayerController.Instance.ExchangeItem(this);
            }
            else
            {
                PlayerController.Instance.GetItem(this);
            }
        }

        protected abstract void OnFoward();
        protected abstract void OnRewind();

        private void Foward()
        {
            var currentObj = this.gameObject;
            var newObj = InteractableItemSO.FutureObject;
            StartCoroutine(RunAnimation(currentObj, newObj));
        }

        private void Rewind()
        {
            var currentObj = this.gameObject;
            var newObj = InteractableItemSO.PastObject;
            StartCoroutine(RunAnimation(currentObj, newObj));
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

        public bool Equals(Item other)
        {
            return InteractableItemSO.name == other.InteractableItemSO.name;
        }
    }
}