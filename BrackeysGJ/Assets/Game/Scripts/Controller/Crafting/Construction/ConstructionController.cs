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
                var finalPos = RaycastHit.point + (Vector3.up * StructurePlaceholder.transform.localScale.y);
                StructurePlaceholder.transform.position = finalPos;
            }

            if(Input.GetKeyDown(KeyCode.F))
                Instantiate(SelectedCell.Recipe.Item.Prefab, StructurePlaceholder.transform.position, Quaternion.identity);
        }
    }
}