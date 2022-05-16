using System;
using BrackeysGJ.Assets.Game.Scripts.Controller.Crafting;
using Game.Scripts.Controller.Crafting;
using Game.Scripts.Domain.Crafting;
using UnityEngine;

namespace Game.Scripts.Controller.Construction
{   
    public class ConstructionController : BaseCraftingController
    {
        [SerializeField]
        private Transform CamChild;
        
        private RaycastHit RaycastHit;
        private CraftingCellController SelectedCell;
        private GameObject StructurePlaceholder;

        protected override CraftingList GetCraftingList() => CraftingService.ConstructionCraftingList;

        protected override void OnShow()
        {
            SelectRecipe(CraftingCells[0]);
        }

        protected override void OnHide()
        {
            Destroy(StructurePlaceholder);
            StructurePlaceholder = null;
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
            Destroy(StructurePlaceholder);
            StructurePlaceholder = Instantiate(SelectedCell.Recipe.Item.Prefab);//TODO: colocar material diferente
        }

        private void Update()
        {
            if(StructurePlaceholder == null)
                return;
                
            if(Physics.Raycast(CamChild.position, CamChild.forward, out RaycastHit, 6f))
            {
                var finalPos =  RaycastHit.point + (Vector3.up * StructurePlaceholder.transform.localScale.y);
                finalPos = new Vector3(Mathf.Round(finalPos.x), finalPos.y, Mathf.Round(finalPos.z));
                StructurePlaceholder.transform.position = finalPos;
            }

            if(Input.GetKeyDown(KeyCode.F))
                Instantiate(SelectedCell.Recipe.Item.Prefab, StructurePlaceholder.transform.position, StructurePlaceholder.transform.rotation);

            var scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput != 0)
                StructurePlaceholder.transform.Rotate(Vector3.up * (Mathf.Sign(scrollInput) * 45), Space.Self);
        }
    }
}