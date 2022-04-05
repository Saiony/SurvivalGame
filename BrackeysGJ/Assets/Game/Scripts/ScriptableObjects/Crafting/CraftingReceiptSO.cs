using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects.Crafting
{
    [CreateAssetMenu(fileName = "CraftingReceipt", menuName = "ScriptableObjects/CraftingReceipt", order = 1)]
    public class CraftingReceiptSO : ScriptableObject
    {
        public ItemSO Item;
        public List<ItemSO> Materials;
        public List<int> MaterialsQuantity;

        [TextArea(1, 3)]
        public string Description;
    }
}
