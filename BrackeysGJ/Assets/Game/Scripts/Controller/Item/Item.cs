using System;
using System.Collections;
using Game.ScriptableObjects;
using Game.Scripts.Controller.Interact;
using Game.Scripts.Controller.Interface.Item;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.Controller.Item
{
    public abstract class Item : Interactable, ITimeChangeable
    {
        public bool Fowardable => InteractableItemSO.FutureObject != null;
        public bool Rewindable => InteractableItemSO.PastObject != null;
        public InteractableItemSO InteractableItemSO;
        
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
            yield return StartCoroutine(Shrink(objToDestroy));
            Destroy(objToDestroy);

            yield return StartCoroutine(Shrink(objToInstantiate, false));
            yield return StartCoroutine(Expand(objToInstantiate));
        }

        private IEnumerator Shrink(GameObject go, bool animate = true)
        {
            yield return null;
            // bool animate diz se o Shrink vai ter um tempo pra diminuir
            // Todo: @Mike
        }

        private IEnumerator Expand(GameObject go, bool animate = true)
        {
            yield return null;
            // Todo: @Mike
        }
    }
}