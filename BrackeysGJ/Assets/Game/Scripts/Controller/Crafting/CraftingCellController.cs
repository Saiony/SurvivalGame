using System;
using Game.Scripts.Domain.Crafting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Game.Scripts.Controller.Player;
using System.Linq;

namespace BrackeysGJ.Assets.Game.Scripts.Controller.Crafting
{
    public class CraftingCellController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Image Icon;

        public CraftingReceipt Receipt;
        ICraftingCellListener Listener;
        IInventory PlayerInventory;

        public void Init(CraftingReceipt receipt, ICraftingCellListener listener)
        {
            Receipt = receipt;
            Icon.sprite = Receipt.Item.Image;
            Listener = listener;
            PlayerInventory = PlayerController.Instance.Items.Inventory;
            UpdateSillhouette();
        }

        public void Update()
        {
            UpdateSillhouette();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
           Listener.OnCellPointerEnter(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Listener.OnCellPointerExit();
        }

        private void UpdateSillhouette()
        {
            //TODO: pegar inventory no Init
            bool canCraft = true;
            Receipt.Materials.ForEach(material => 
            {
                var playerQuantity = PlayerInventory.Items.Where(x => x?.Id == material?.Item.Id).Select(x => x.Quantity).Sum();
                if(playerQuantity < material.Quantity)
                {
                    canCraft = false;
                    return;
                }
            });
            Icon.color = canCraft ? Color.white : Color.black;
        }
    }
}