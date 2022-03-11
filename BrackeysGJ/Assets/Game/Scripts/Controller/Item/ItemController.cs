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

        public string Id => ItemSO.name;
        public Item Item { get; private set; }

        private void Awake()
        {
            switch (ItemSO)
            {
                case MiscSO misc:
                    Item = new Misc(ItemSO.Id, ItemSO.Name, ItemSO.Description, ItemSO.Image, 1);
                    break;
                case ConsumableSO consumableSO:
                    Item = new Consumable(consumableSO.Id, consumableSO.name, consumableSO.Description, consumableSO.Image, 
                                          consumableSO.Command, consumableSO.HungerSatisfied, consumableSO.HealthGiven);
                    break;
                default:
                    throw new InvalidOperationException("Invalid ItemSO type: " +typeof(ItemSO));
            }
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