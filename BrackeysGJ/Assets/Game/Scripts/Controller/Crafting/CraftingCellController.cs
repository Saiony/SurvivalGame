using System;
using Game.Scripts.Domain.Crafting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Game.Scripts.Controller.Player;
using System.Linq;
using System.Collections;

namespace BrackeysGJ.Assets.Game.Scripts.Controller.Crafting
{
    public class CraftingCellController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField]
        private Image Icon;

        public CraftingReceipt Receipt;
        private ICraftingCellListener Listener;
        private IInventory PlayerInventory;
        private WaitForSeconds CraftingTime;
        private bool Crafting = false;
        

        public void Init(CraftingReceipt receipt, ICraftingCellListener listener)
        {
            Receipt = receipt;
            Icon.sprite = Receipt.Item.Image;
            Listener = listener;
            PlayerInventory = PlayerController.Instance.Items.Inventory;
            CraftingTime = new WaitForSeconds(2);
            UpdateSillhouette();
        }

        public void Update()
        {
            UpdateSillhouette();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.button != PointerEventData.InputButton.Right)
                return;
            if(Crafting)
                return;
            if(!PlayerController.Instance.CanCraft(Receipt))
                return;
            
            Debug.Log("caue - crafting started");
            StartCoroutine(StartCraftingAnimation(Craft));
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
            var canCraft = PlayerController.Instance.CanCraft(Receipt);
            Icon.color = canCraft ? Color.white : Color.black;
        }

        private IEnumerator StartCraftingAnimation(Action callback)
        {
            Crafting = true;
            PlayerController.Instance.PlayCraftingAnimation();
            yield return CraftingTime;
            PlayerController.Instance.StopCraftingAnimation();
            callback();
        }

        private void Craft()
        {
            Debug.Log("caue - Craft!!");
            PlayerController.Instance.Craft(Receipt);
            Crafting = false;
        }
    }
}