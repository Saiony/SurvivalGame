using System;
using Game.Scripts.Domain.Crafting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Game.Scripts.Controller.Player;
using System.Collections;
using Game.Scripts.Controller.Crafting;

namespace BrackeysGJ.Assets.Game.Scripts.Controller.Crafting
{
    public class CraftingCellController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField]
        private Image Icon;

        [SerializeField]
        private Image SelectedImage;

        public CraftingRecipe Recipe;
        private ICraftingCellListener Listener;
        

        public void Init(CraftingRecipe receipt, ICraftingCellListener listener)
        {
            Recipe = receipt;
            Icon.sprite = Recipe.Item.Image;
            Listener = listener;
            UpdateSillhouette();
        }

        public void Update()
        {
            UpdateSillhouette();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
                Listener.OnCellLeftClick(this);
            if(eventData.button == PointerEventData.InputButton.Right)
                Listener.OnCellRightClick(Recipe);
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
            var canCraft = PlayerController.Instance.CanCraft(Recipe);
            Icon.color = canCraft ? Color.white : Color.black;
        }

        public void Select()
        {
            SelectedImage.gameObject.SetActive(true);
        }

        public void Unselect()
        {
            SelectedImage.gameObject.SetActive(false);
        }
    }
    
    public interface ICraftingCellListener
    {
        void OnCellPointerEnter(CraftingCellController cell);
        void OnCellPointerExit();
        void OnCellLeftClick(CraftingCellController cell);
        void OnCellRightClick(CraftingRecipe recipe);
    }
}