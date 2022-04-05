using System;
using System.Collections.Generic;
using Game.Scripts.Helper;
using Game.Scripts.ScriptableObjects;
using Game.Scripts.ScriptableObjects.Crafting;

namespace Game.Scripts.Domain.Crafting
{
    public class CraftingReceipt
    {
        public ItemSO Item { get; private set; }
        public List<CraftingMaterial> Materials { get; private set; }
        public string Description { get; private set; }

        public CraftingReceipt(CraftingReceiptSO so)
        {
            SetItem(so.Item);
            SetMaterials(so.Materials, so.MaterialsQuantity);
            SetDescription(so.Description);
        }

        private void SetItem(ItemSO item)
        {
            if(item == null)
                throw new InvalidOperationException("Item can't be null");
            
            Item = item;
        }

        private void SetMaterials(List<ItemSO> itemMaterials, List<int> quantity)
        {
            if(itemMaterials.IsNullOrEmpty())
                throw new InvalidOperationException("Materials can't be null or empty");
            if(quantity.IsNullOrEmpty())
                throw new InvalidOperationException("Quantity can't be null or empty");
            if(itemMaterials.Count != quantity.Count)
                throw new InvalidOperationException($"Materials and Quantity must have the same count");
            
            Materials = new List<CraftingMaterial>();
            for (int i = 0; i < itemMaterials.Count; i++)
            {
                var material = new CraftingMaterial(itemMaterials[i], quantity[i]);
                Materials.Add(material);
            }
        }

        private void SetDescription(string description)
        {
            if(description == null)
                throw new InvalidOperationException("Description can't be null");
            
            Description = description;
        }
    }
}