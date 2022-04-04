using System.Collections.Generic;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace BrackeysGJ.Assets.Game.Scripts.Controller.Crafting
{    
    [CreateAssetMenu(fileName = "CraftingItem", menuName = "BrackeysGJ/CraftingItem", order = 0)]
    public class CraftingItemSO : ScriptableObject 
    {
        [SerializeField]
        private ItemSO ItemSO;

        [SerializeField]
        private List<ItemSO> CraftingMaterials;

        [SerializeField]
        private List<int> CraftingMaterialsQuantity;
    }
}