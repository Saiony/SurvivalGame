using System.Collections.Generic;
using BrackeysGJ.Assets.Game.Scripts.Controller.Crafting;
using Game.Scripts.Domain.Crafting;
using Game.Scripts.Service;
using Game.Scripts.Service.Interface;
using UnityEngine;

namespace Game.Scripts.Controller.Crafting
{
    public abstract class BaseCraftingController : MonoBehaviour, ICraftingCellListener
    {
        [SerializeField]
        private CanvasGroup Content;

        [SerializeField]
        private CraftingCellController CellPrefab;

        [SerializeField]
        private CraftingInfoController CraftingInfo;

        protected ICraftingService CraftingService;
        protected List<CraftingCellController> CraftingCells;

        public void Init() 
        {
            CraftingInfo.Init();
            CraftingService = ServiceProvider.Instance.Get<ICraftingService>();
            LoadList();
            OnInit();
        }

        public void Show()
        {
            Content.gameObject.SetActive(true);
            CraftingCells.ForEach(x => x.Update());
            OnShow();
        }

        public void Hide()
        {
            Content.gameObject.SetActive(false);
        }

        private void LoadList()
        {
            CraftingCells = new List<CraftingCellController>();
            GetCraftingList().Recipes.ForEach(receipt => 
            {
                var cell = Instantiate(CellPrefab, Content.transform);
                cell.Init(receipt, this);
                CraftingCells.Add(cell);
            });
        }

        public void OnCellPointerEnter(CraftingCellController cell)
        {
            CraftingInfo.Show(cell.Recipe, cell.transform.position);
        }

        public void OnCellPointerExit()
        {
            CraftingInfo.Hide();
        }

        public void OnCellLeftClick(CraftingCellController cell)
        {
            OnCraftingCellLeftClick(cell);
        }

        public void OnCellRightClick(CraftingRecipe recipe)
        {
            OnCraftingCellRightClick(recipe);
        }

        protected abstract CraftingList GetCraftingList();
        protected virtual void OnInit(){}
        protected virtual void OnShow(){}
        protected virtual void OnCraftingCellLeftClick(CraftingCellController cell){}
        protected virtual void OnCraftingCellRightClick(CraftingRecipe recipe){}
    }
}
