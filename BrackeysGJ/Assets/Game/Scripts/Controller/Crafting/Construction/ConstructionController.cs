using System;
using BrackeysGJ.Assets.Game.Scripts.Controller.Crafting;
using Game.Scripts.Controller.Crafting;
using Game.Scripts.Controller.Crafting.Construction;
using Game.Scripts.Domain.Crafting;
using Game.Scripts.Domain.Items;
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
        private ConstructionPlaceholderController ConstructionPlaceholder;

        protected override CraftingList GetCraftingList() => CraftingService.ConstructionCraftingList;

        protected override void OnShow()
        {
            ConstructionPlaceholder = Instantiate(StructPlaceholderPrefab);
            SelectRecipe(CraftingCells[0]);
        }

        protected override void OnHide()
        {
            Destroy(ConstructionPlaceholder.gameObject);
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

            var structure = SelectedCell.Recipe.Item;
            var meshesHolder = structure.Prefab.GetComponent<BuildingController>().MeshesHolder;
            ConstructionPlaceholder.Init(meshesHolder, (ConstructionStructure)structure);
        }

        public GameObject raycastHit;

        private void Update()
        {
            if (ConstructionPlaceholder == null)
                return;

            Debug.DrawRay(CamChild.position + (CamChild.forward * 3), CamChild.forward, Color.green);
            if (Physics.Raycast(CamChild.position + (CamChild.forward * 3), CamChild.forward, out RaycastHit, 10f))
            {
                raycastHit = RaycastHit.collider.gameObject;
                var finalPos = RaycastHit.point;
                finalPos = new Vector3(
                                        Mathf.Round(finalPos.x),
                                        Mathf.Round(finalPos.y),
                                        Mathf.Round(finalPos.z)
                                      );

                //offsets position closer to player
                var diffX = CamChild.position.x - finalPos.x;
                var diffZ = CamChild.position.z - finalPos.z;
                if (Mathf.Abs(diffX) > Mathf.Abs(diffZ)) //modifica valor em x
                {
                    var dirX = diffX < 0 ? -1 : 1;
                    var offset = (ConstructionPlaceholder.Structure.Size.z * ((float)dirX / 2));
                    finalPos.x += (int)offset;
                }
                else //modifica valor em z
                {
                    var dirZ = diffZ < 0 ? -1 : 1;
                    var offset = (ConstructionPlaceholder.Structure.Size.z * ((float)dirZ / 2));
                    finalPos.z += (int)offset;
                }

                ConstructionPlaceholder.transform.position = finalPos;
            }

            if (Input.GetKeyDown(KeyCode.F))
                Instantiate(SelectedCell.Recipe.Item.Prefab, ConstructionPlaceholder.transform.position, ConstructionPlaceholder.MeshesHolder.transform.rotation);

            var scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput != 0)
                ConstructionPlaceholder.Rotate(scrollInput);
        }
    }
}