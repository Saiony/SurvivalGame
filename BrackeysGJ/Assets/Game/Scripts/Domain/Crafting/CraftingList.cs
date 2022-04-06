using System;
using System.Collections.Generic;
using System.Linq;
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
                var craftingReceipt = new CraftingReceipt(craftingItemSO);
                Receipts.Add(craftingReceipt);
            });
        }
    }
}