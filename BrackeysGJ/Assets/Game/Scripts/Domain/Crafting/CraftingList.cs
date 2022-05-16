using System;
using System.Collections.Generic;
using Game.Scripts.Domain.Items;
using Game.Scripts.Helper;
using Game.Scripts.ScriptableObjects.Crafting;

namespace Game.Scripts.Domain.Crafting
{
    public class CraftingList
    {
        public List<CraftingRecipe> Recipes { get; private set; }

        public CraftingList(CraftingListSO craftingListSO)
        {
            SetCraftingList(craftingListSO);
        }

        private void SetCraftingList(CraftingListSO craftingListSO)
        {
            if(craftingListSO == null || craftingListSO.Receipts.IsNullOrEmpty())
                throw new InvalidOperationException("CraftingList can't be null or empty");

            Recipes = new List<CraftingRecipe>();
            craftingListSO.Receipts.ForEach(craftingItemSO => 
            {
                var item = ItemsHelper.CreateItem(craftingItemSO.Item);
                var materials = new List<Item>();
                craftingItemSO.Materials.ForEach(x => materials.Add(ItemsHelper.CreateItem(x)));
                
                var craftingReceipt = new CraftingRecipe(item, materials, craftingItemSO.MaterialsQuantity);
                Recipes.Add(craftingReceipt);
            });
        }
    }
}