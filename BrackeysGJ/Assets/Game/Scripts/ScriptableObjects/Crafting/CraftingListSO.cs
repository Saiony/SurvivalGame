using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects.Crafting
{
    [CreateAssetMenu(fileName = "CraftingList", menuName = "ScriptableObjects/Crafting/List", order = 2)]
    public class CraftingListSO : ScriptableObject
    {
        public List<CraftingReceiptSO> Receipts;
    }
}