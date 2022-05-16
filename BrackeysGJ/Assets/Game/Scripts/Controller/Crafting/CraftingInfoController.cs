using Game.Scripts.Domain.Crafting;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Game.Scripts.Controller.Player;
using System.Linq;
using Game.Scripts.Domain.Items;

namespace BrackeysGJ.Assets.Game.Scripts.Controller.Crafting
{
    public class CraftingInfoController : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup Modal;

        [SerializeField]
        private Transform MaterialsContent;

        [SerializeField]
        private CraftingMaterialController CraftingMaterialPrefab;

        [SerializeField]
        private TextMeshProUGUI ItemName;

        [SerializeField]
        private TextMeshProUGUI ItemStats;

        [SerializeField]
        private TextMeshProUGUI ItemDescription;

        List<CraftingMaterialController> MaterialsController;

        public void Init()
        {
            Hide();
            MaterialsController = new List<CraftingMaterialController>();
        }

        public void Show(CraftingRecipe recipe, Vector3 pos)
        {
            ItemName.text = recipe.Item.Name;
            ItemDescription.text = recipe.Item.Description;
            ItemStats.text = CreateItemStatsText(recipe.Item);
            var playerInventory = PlayerController.Instance.Items.Inventory;
            transform.position = pos;

            for (int i = 0; i < recipe.Materials.Count; i++)
            {
                if(i > MaterialsController.Count - 1)
                {
                    var materialController = Instantiate(CraftingMaterialPrefab, MaterialsContent);
                    materialController.Init();
                    MaterialsController.Add(materialController);
                }

                var playerItems = playerInventory.Items.Where(x => x?.Id == recipe.Materials[i]?.Item.Id);
                var playerQuantity = playerItems == null ? 0 : playerItems.Select(x => x.Quantity).Sum();
                MaterialsController[i].Show(recipe.Materials[i], playerQuantity);
            }

            for (int i = recipe.Materials.Count; i < MaterialsController.Count; i++)
                MaterialsController[i].Hide();
                            
            Modal.gameObject.SetActive(true);
        }

        public void Hide()
        {
            Modal.gameObject.SetActive(false);
        }

        private string CreateItemStatsText(Item item)
        {
            var stats = string.Empty;
            switch (item)
            {
                case Weapon weapon:
                    foreach(KeyValuePair<DamageType, int> damage in weapon.Attack.Damages)
                        stats += $"{damage.Key.ToString()} - {damage.Value.ToString()} \n";
                    break;
                default:
                    break;
            }
            return stats;
        }
    }
}