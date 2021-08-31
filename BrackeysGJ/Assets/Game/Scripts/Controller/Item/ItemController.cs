using System;
using System.Collections;
using BrackeysGJ.Assets.Game.Scripts.Domain.Items;
using DG.Tweening;
using Game.Scripts.Controller.Player;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.Controller.Itens
{
    public class ItemController : MonoBehaviour
    {
        [SerializeField]
        private ItemSO _itemSO = null;
        private ItemSO ItemSO => _itemSO;

        [SerializeField]
        private Collider _detectionCollider = null;
        public Collider DetectionCollider => _detectionCollider;

        public string Id => ItemSO.name;
        public Item Item { get; private set; }

        private void Awake()
        {
            Item = new Misc(ItemSO.Id, ItemSO.Name, ItemSO.Description, ItemSO.Image, 1);
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
            return ItemSO.name == other.ItemSO.name;
        }
    }
}