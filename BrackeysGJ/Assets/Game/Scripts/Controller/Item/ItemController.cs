using System;
using System.Collections;
using DG.Tweening;
using Game.Scripts.Controller.Interact;
using Game.Scripts.Controller.Player;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.Controller.Itens
{
    public class ItemController : Interactable
    {
        [SerializeField]
        private ItemSO _interactableItemSO = null;
        private ItemSO InteractableItemSO => _interactableItemSO;

        [SerializeField]
        private Collider _collider = null;
        private Collider Collider => _collider;

        [SerializeField]
        private Transform _feet = null;
        public Transform Feet => _feet;
        public string Id => InteractableItemSO.name;

        protected override void OnInteract(Vector3 pos)
        {
            Debug.Log("Player interacted with Item");
            if (PlayerController.Instance.HasItem)
                return;

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