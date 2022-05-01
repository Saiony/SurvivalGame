using System;
using System.Collections.Generic;
using System.Linq;
using BrackeysGJ.Assets.Game.Scripts.Domain.Items;
using Game.Scripts.Helper;
using Game.Scripts.ScriptableObjects;
using Game.Scripts.ScriptableObjects.Crafting;

namespace Game.Scripts.Domain.Crafting
{
    public class CraftingReceipt
    {
        public Item Item { get; private set; }
        public List<CraftingMaterial> Materials { get; private set; }

        public CraftingReceipt(Item item, List<Item> materials, List<int> materialsQuantity)
        {
            SetItem(item);
            SetMaterials(materials.ToList(), materialsQuantity.ToList());
        }

        private void SetItem(Item item)
        {
            if(item == null)
                throw new InvalidOperationException("Item can't be null");
            
            Item = item;
        }

        private void SetMaterials(List<Item> itemMaterials, List<int> quantity)
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
    }
}