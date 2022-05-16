using Game.Scripts.Domain.Crafting;
using Game.Scripts.ScriptableObjects.Crafting;
using Game.Scripts.Service.Interface;
using UnityEngine;

namespace Game.Scripts.Service
{
    public class CraftingService : ICraftingService
    {
        public CraftingList TabCraftingList { get; private set; }
        public CraftingList ConstructionCraftingList { get; private set; }

        public CraftingService()
        {
            var craftingListSO = Resources.Load("CraftingList") as CraftingListSO;
            TabCraftingList = new CraftingList(craftingListSO);

            var constructionCraftingListSO = Resources.Load("ConstructionList") as CraftingListSO;
            ConstructionCraftingList = new CraftingList(constructionCraftingListSO);
        }
    }
}