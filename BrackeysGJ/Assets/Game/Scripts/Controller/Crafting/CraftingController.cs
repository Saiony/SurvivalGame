using System.Collections.Generic;
using Game.Scripts.Domain.Crafting;
using Game.Scripts.ScriptableObjects.Crafting;
using Game.Scripts.Service;
using Game.Scripts.Service.Interface;
using UnityEngine;

namespace BrackeysGJ.Assets.Game.Scripts.Controller.Crafting
{
    public class CraftingController : MonoBehaviour, ICraftingCellListener
    {
        [SerializeField]
        private CanvasGroup Content;

        [SerializeField]
        private CraftingCellController CellPrefab;

        [SerializeField]
        private CraftingInfoController CraftingInfo;

        private ICraftingService CraftingService;
        private List<CraftingCellController> CraftingCells;

        public void Init() 
        {
            CraftingInfo.Init();
            CraftingService = ServiceProvider.Instance.Get<ICraftingService>();
            LoadList();
        }

        public void Show()
        {
            Content.gameObject.SetActive(true);
            CraftingCells.ForEach(x => x.Update());
        }

        public void Hide()
        {
            Content.gameObject.SetActive(false);
        }

        private void LoadList()
        {
            CraftingCells = new List<CraftingCellController>();
            CraftingService.CraftingList.Receipts.ForEach(receipt => 
            {
                var cell = Instantiate(CellPrefab, Content.transform);
                cell.Init(receipt, this);
                CraftingCells.Add(cell);
            });
        }

        public void OnCellPointerEnter(CraftingCellController cell)
        {
            CraftingInfo.Show(cell.Receipt, cell.transform.position);
        }

        public void OnCellPointerExit()
        {
            CraftingInfo.Hide();
        }
    }

    public interface ICraftingCellListener
    {
        void OnCellPointerEnter(CraftingCellController cell);
        void OnCellPointerExit();
    }
}