using DG.Tweening;
using Game.Scripts.Domain.Interface.Items;
using UnityEngine;

namespace BrackeysGJ.Assets.Game.Scripts.Controller.Inventory
{
    public class EquipItemDisplayController : BaseItemDisplayController
    {
        [SerializeField]
        private Sprite _blockedImage;
        private Sprite BlockedImage => _blockedImage;

        [SerializeField]
        private Sprite _placeHolderImage;
        private Sprite PlaceHolderImage => _placeHolderImage;

        public EquipmentSlot Slot { get; private set; }

        public void Init(EquipmentSlot slot, InventoryItemDisplayListener listener)
        {
            Slot = slot;
            BaseInit(listener);
        }

        public override void OnItemAdded(IItem item)
        {
            PlayerItems.EquippedItems.AddEquipment(Slot, item as IEquipment);
        }

        protected override void OnItemCleared()
        {
            DisplayImage.sprite = PlaceHolderImage;
            Sequence seq = DOTween.Sequence();
            seq.Append(DisplayImage.DOFade(0.8f, 0));
            seq.Play();
        }

        public void Block()
        {
            SelectButton.interactable = false;
            DisplayImage.DOFade(1, 0).Play();
        }

        public void Unblock()
        {
            SelectButton.interactable = true;
            DisplayItem(ItemDisplayed);
        }
    }
}
