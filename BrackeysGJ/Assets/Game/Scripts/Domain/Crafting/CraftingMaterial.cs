using System;
using BrackeysGJ.Assets.Game.Scripts.Domain.Items;
using Game.Scripts.Helper;
using Game.Scripts.ScriptableObjects;

namespace Game.Scripts.Domain.Crafting
{
    public class CraftingMaterial
    {
        public Item Item { get; private set; }
        public int Quantity { get; private set; }

        public CraftingMaterial(ItemSO itemSO, int quantity)
        {
            SetItem(itemSO);
            SetQuantity(quantity);
        }

        private void SetItem(ItemSO itemSO)
        {
            if(itemSO == null)
                throw new InvalidOperationException("Item can't be null");
            
            Item = ItemsHelper.CreateItem(itemSO);
        }

        private void SetQuantity(int quantity)
        {
            if(quantity <= 0)
                throw new InvalidOperationException("Quantity must be a positive value");
            
            Quantity = quantity;
        }
    }
}