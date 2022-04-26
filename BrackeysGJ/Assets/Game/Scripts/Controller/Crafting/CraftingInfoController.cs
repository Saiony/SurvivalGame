using Game.Scripts.Domain.Crafting;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Game.Scripts.Controller.Player;
using System.Linq;
using Game.Scripts.ScriptableObjects;

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

        public void Show(CraftingReceipt receipt, Vector3 pos)
        {
            ItemName.text = receipt.Item.Name;
            ItemDescription.text = receipt.Item.Description;
            ItemStats.text = CreateItemStatsText(receipt.Item);
            var playerInventory = PlayerController.Instance.Items.Inventory;
            transform.position = new Vector3(transform.position.x, pos.y, transform.position.z);

            for (int i = 0; i < receipt.Materials.Count; i++)
            {
                if(i > MaterialsController.Count - 1)
                {
                    var materialController = Instantiate(CraftingMaterialPrefab, MaterialsContent);
                    materialController.Init();
                    MaterialsController.Add(materialController);
                }

                var playerItems = playerInventory.Items.Where(x => x?.Id == receipt.Materials[i]?.Item.Id);
                var playerQuantity = playerItems == null ? 0 : playerItems.Select(x => x.Quantity).Sum();
                MaterialsController[i].Show(receipt.Materials[i], playerQuantity);
            }

            for (int i = receipt.Materials.Count; i < MaterialsController.Count; i++)
                MaterialsController[i].Hide();
                            
            Modal.gameObject.SetActive(true);
        }

        public void Hide()
        {
            Modal.gameObject.SetActive(false);
        }

        private string CreateItemStatsText(ItemSO item)
        {
            var stats = string.Empty;
            switch (item)
            {
                case WeaponSO weapon:
                    for (int i = 0; i < weapon.DamagesType.Count; i++)
                        stats += $"{weapon.DamagesType[i].ToString()} - {weapon.DamagesValue[i].ToString()} \n";
                    break;
                default:
                    break;
            }
            return stats;
        }
    }
}