using Game.Scripts.Domain.Crafting;
using Game.Scripts.ScriptableObjects.Crafting;
using Game.Scripts.Service.Interface;
using UnityEngine;

namespace Game.Scripts.Service
{
    public class CraftingService : ICraftingService
    {
        public CraftingList CraftingList { get; private set; }

        public CraftingService()
        {
            var craftingListSO = Resources.Load("CraftingList") as CraftingListSO;
            CraftingList = new CraftingList(craftingListSO);
        }
    }
}