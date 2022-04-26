using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects.Crafting
{
    [CreateAssetMenu(fileName = "CraftingReceipt", menuName = "ScriptableObjects/Crafting/Receipt", order = 1)]
    public class CraftingReceiptSO : ScriptableObject
    {
        public ItemSO Item;
        public List<ItemSO> Materials;
        public List<int> MaterialsQuantity;
    }
}
