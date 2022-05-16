using System;
using System.Collections;
using BrackeysGJ.Assets.Game.Scripts.Domain.Items;
using DG.Tweening;
using Game.Scripts.Domain.Interface.Items;
using Game.Scripts.Domain.Items;
using Game.Scripts.Helper;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.Controller.Itens
{
    public class ItemController : MonoBehaviour
    {
        [SerializeField]
        private ItemSO _itemSO = null;
        private ItemSO ItemSO => _itemSO;

        public string Id => ItemSO.name;
        public IItem Item { get; private set; }

        private void Awake()
        {
            Item = ItemsHelper.CreateItem(ItemSO);
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

            return seq;
        }

        private IEnumerator Expand(GameObject go, bool animate = true)
        {
            yield return null;
        }

        public bool Equals(ItemController other)
        {
            return ItemSO.name == other.ItemSO.name;
        }
    }
}