using System;
using System.Collections.Generic;
using System.Linq;
using BrackeysGJ.Assets.Game.Scripts.Domain.Items;
using Game.Scripts.Helper;
using Game.Scripts.ScriptableObjects.Crafting;

namespace Game.Scripts.Domain.Crafting
{
    public class CraftingList
    {
        public List<CraftingReceipt> Receipts { get; private set; }

        public CraftingList(CraftingListSO craftingListSO)
        {
            SetCraftingList(craftingListSO);
        }

        private void SetCraftingList(CraftingListSO craftingListSO)
        {
            if(craftingListSO == null || craftingListSO.Receipts.IsNullOrEmpty())
                throw new InvalidOperationException("CraftingList can't be null or empty");

            Receipts = new List<CraftingReceipt>();
            craftingListSO.Receipts.ForEach(craftingItemSO => 
            {
                var item = ItemsHelper.CreateItem(craftingItemSO.Item);
                var materials = new List<Item>();
                craftingItemSO.Materials.ForEach(x => materials.Add(ItemsHelper.CreateItem(x)));
                
                var craftingReceipt = new CraftingReceipt(item, materials, craftingItemSO.MaterialsQuantity);
                Receipts.Add(craftingReceipt);
            });
        }
    }
}