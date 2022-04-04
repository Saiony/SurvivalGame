using System.Collections.Generic;
using UnityEngine;

namespace BrackeysGJ.Assets.Game.Scripts.Controller.Crafting
{
    public class CraftingListSO : ScriptableObject
    {
        [SerializeField]
        private List<CraftingItemSO> CraftingItems;
    }
}