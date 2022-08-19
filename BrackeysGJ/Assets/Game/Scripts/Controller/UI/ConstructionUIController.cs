using System;
using BrackeysGJ.Assets.Game.Scripts.Controller.Crafting;
using Game.Scripts.Controller.Crafting;
using Game.Scripts.Controller.Crafting.Construction;
using Game.Scripts.Controller.Player;
using Game.Scripts.Domain.Crafting;
using Game.Scripts.Domain.Items;
using UnityEngine;

namespace Game.Scripts.Controller.UI
{
    public class ConstructionUIController : BaseCraftingController
    {
        private CraftingCellController SelectedCell;

        protected override CraftingList GetCraftingList() => CraftingService.ConstructionCraftingList;

        protected override void OnShow()
        {
            PlayerController.Instance.StartConstructionMode();
            SelectRecipe(CraftingCells[0]);
        }

        protected override void OnHide()
        {
            PlayerController.Instance.EndConstructionMode();
            SelectedCell = null;
        }

        protected override void OnCraftingCellLeftClick(CraftingCellController cell)
        {
            SelectRecipe(cell);
        }

        private void SelectRecipe(CraftingCellController cell)
        {
            if (SelectedCell == cell)
                return;

            SelectedCell?.Unselect();
            SelectedCell = cell;
            cell.Select();

            PlayerController.Instance.SelectBuildingRecipe(SelectedCell.Recipe);
        }
    }
}