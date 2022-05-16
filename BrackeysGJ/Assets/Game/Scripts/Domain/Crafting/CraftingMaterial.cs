using System;
using Game.Scripts.Domain.Interface.Items;
using Game.Scripts.Domain.Items;

namespace Game.Scripts.Domain.Crafting
{
    public class CraftingMaterial
    {
        public IItem Item { get; private set; }
        public int Quantity { get; private set; }

        public CraftingMaterial(Item item, int quantity)
        {
            SetItem(item);
            SetQuantity(quantity);
        }

        private void SetItem(Item item)
        {
            if(item == null)
                throw new InvalidOperationException("Item can't be null");
            
            Item = item;
        }

        private void SetQuantity(int quantity)
        {
            if(quantity <= 0)
                throw new InvalidOperationException("Quantity must be a positive value");
            
            Quantity = quantity;
        }
    }
}