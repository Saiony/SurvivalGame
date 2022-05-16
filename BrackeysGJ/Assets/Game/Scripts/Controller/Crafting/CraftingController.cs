using System;
using System.Collections;
using Game.Scripts.Controller.Player;
using Game.Scripts.Domain.Crafting;
using UnityEngine;

namespace Game.Scripts.Controller.Crafting
{
    public class CraftingController : BaseCraftingController
    {
        private IInventory PlayerInventory;
        private WaitForSeconds CraftingTime;
        private bool Crafting = false;

        protected override void OnInit()
        {
            PlayerInventory = PlayerController.Instance.Items.Inventory;
            CraftingTime = new WaitForSeconds(2);
        }
        
        protected override CraftingList GetCraftingList()
        {
            return CraftingService.TabCraftingList;
        }

        protected override void OnCraftingCellRightClick(CraftingRecipe recipe)
        {
            if(Crafting)
                return;
            if(!PlayerController.Instance.CanCraft(recipe))
                return;
            
            Debug.Log("caue - crafting started");
            StartCoroutine(StartCraftingAnimation(() => Craft(recipe)));
        }
        
        private void Craft(CraftingRecipe recipe)
        {
            Debug.Log("caue - Craft!!");
            PlayerController.Instance.Craft(recipe);
            Crafting = false;
        }

        private IEnumerator StartCraftingAnimation(Action callback)
        {
            Crafting = true;
            PlayerController.Instance.PlayCraftingAnimation();
            yield return CraftingTime;
            PlayerController.Instance.StopCraftingAnimation();
            callback();
        }
    }
}