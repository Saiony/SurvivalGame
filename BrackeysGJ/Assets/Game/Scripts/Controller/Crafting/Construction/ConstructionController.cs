using System;
using BrackeysGJ.Assets.Game.Scripts.Controller.Crafting;
using Game.Scripts.Controller.Crafting;
using Game.Scripts.Controller.Crafting.Construction;
using Game.Scripts.Domain.Crafting;
using UnityEngine;

namespace Game.Scripts.Controller.Construction
{
    public class ConstructionController : BaseCraftingController
    {
        [SerializeField]
        private Transform CamChild;

        [SerializeField]
        private ConstructionPlaceholderController StructPlaceholderPrefab;


        private RaycastHit RaycastHit;
        private CraftingCellController SelectedCell;
        private ConstructionPlaceholderController StructurePlaceholder;

        protected override CraftingList GetCraftingList() => CraftingService.ConstructionCraftingList;

        protected override void OnShow()
        {
            StructurePlaceholder = Instantiate(StructPlaceholderPrefab);
            SelectRecipe(CraftingCells[0]);
        }

        protected override void OnHide()
        {
            Destroy(StructurePlaceholder.gameObject);
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
            
            var meshFilter = SelectedCell.Recipe.Item.Prefab.GetComponent<BuildingController>().MeshFilter;
            StructurePlaceholder.SetMesh(meshFilter.sharedMesh);
            var meshTransform = meshFilter.gameObject.transform;
            StructurePlaceholder.transform.localScale = meshTransform.localScale;
            StructurePlaceholder.transform.rotation = meshTransform.rotation;
        }

        private void Update()
        {
            if (StructurePlaceholder == null)
                return;

            if (Physics.Raycast(CamChild.position, CamChild.forward, out RaycastHit, 6f))
            {
                var finalPos = RaycastHit.point + (Vector3.up * StructurePlaceholder.MeshFilter.sharedMesh.bounds.size.y * StructurePlaceholder.transform.localScale.y/2);
                finalPos = new Vector3(Mathf.Round(finalPos.x), finalPos.y, Mathf.Round(finalPos.z));
                StructurePlaceholder.transform.position = finalPos;
            }

            if (Input.GetKeyDown(KeyCode.F))
                Instantiate(SelectedCell.Recipe.Item.Prefab, StructurePlaceholder.transform.position, StructurePlaceholder.transform.rotation);

            var scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput != 0)
                StructurePlaceholder.transform.Rotate(Vector3.up * (Mathf.Sign(scrollInput) * 45), Space.Self);
        }
    }
}