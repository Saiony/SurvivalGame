using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Items;
using BrackeysGJ.Assets.Game.Scripts.Domain.PlayerItems;
using DG.Tweening;
using Game.Scripts.Controller.Player;
using Game.Scripts.Controller.Time;
using UnityEngine;
using UnityEngine.Rendering;

namespace BrackeysGJ.Assets.Game.Scripts.Controller.Inventory
{
    public class InventorySceneController : MonoBehaviour, InventoryItemDisplayListener, IInventoryListener, IEquipmentListener
    {
        [SerializeField]
        private InventoryBagController _bag = null;
        private InventoryBagController BagController => _bag;

        [SerializeField]
        private InventoryEquipController _equipment = null;
        private InventoryEquipController EquipmentController => _equipment;

        [SerializeField]
        private InventoryInfoController _itemInfo = null;
        private InventoryInfoController ItemInfo => _itemInfo;

        [SerializeField]
        private CanvasGroup _modal = null;
        private CanvasGroup Modal => _modal;

        [SerializeField]
        private ImageFollowMouse _imageFollowMose = null;
        private ImageFollowMouse ImageFollowMouse => _imageFollowMose;

        private bool Showing { get; set; }
        private BaseItemDisplayController SelectedItemDisplay { get; set; }
        private IPlayerItems PlayerItems { get; set; }
        public static InventorySceneController Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            Modal.alpha = 0;
            Modal.gameObject.SetActive(false);
        }

        private void Start()
        {
            PlayerItems = PlayerController.Instance.Items;
            BagController.Init(this);
            EquipmentController.Init(this);
            ItemInfo.Init();
        }

        public void Toggle()
        {
            if (Showing)
                Hide();
            else
                Show();
        }

        public void OnItemDisplayClickedLeft(BaseItemDisplayController itemDisplay)
        {
            if (SelectedItemDisplay != null)
            {
                //Somente faço um swap e os BaseItem se viram pra enviar isso pro player
                var selectedItemCopy = SelectedItemDisplay.ItemDisplayed;
                DeselectItem(itemDisplay);

                SelectedItemDisplay.SetItem(itemDisplay.ItemDisplayed);
                itemDisplay.SetItem(selectedItemCopy);

                SelectedItemDisplay = null;
                return;
            }
            if (itemDisplay.ItemDisplayed == null)
                return;

            SelectItem(itemDisplay);
        }

        private void SelectItem(BaseItemDisplayController item)
        {
            SelectedItemDisplay = item;
            item.Select();

            ItemInfo.DisplayItem(item.ItemDisplayed);
            ImageFollowMouse.Show(SelectedItemDisplay.ItemDisplayed.Image);

            //bloquear slots inválidos
            EquipmentController.BlockInvalidSlots(SelectedItemDisplay.ItemDisplayed);
        }

        private void DeselectItem(BaseItemDisplayController item)
        {
            ImageFollowMouse.Hide();
            //desbloquear slots inválidos
            EquipmentController.UnblockInvalidSlots(SelectedItemDisplay.ItemDisplayed);

            item.Deselect();
        }

        public void OnItemDisplayClickedRight(BaseItemDisplayController itemDisplay)
        {
            throw new InvalidOperationException("not implemented");
            // var item = itemDisplay.Item;
            // if (!(item is IEquipment))
            //     return;

            // PlayerController.Instance.Items.Equip(item as IEquipment);
        }

        public void OnItemDisplayHovered(BaseItemDisplayController itemDisplay)
        {
            if (SelectedItemDisplay != null)
                return;
            ItemInfo.DisplayItem(itemDisplay.ItemDisplayed);
        }

        private void Show()
        {
            TimeController.Instance.PauseTime();
            InputHandler.Instance.DisableInput();

            PlayerItems.EquippedItems.Subscribe(this);
            PlayerItems.Inventory.Subscribe(this);
            BagController.Display(PlayerItems.Inventory.Items);
            EquipmentController.Display(PlayerItems.EquippedItems.Equipments);
            Showing = true;

            Sequence seq = DOTween.Sequence();
            seq.Append(Modal.DOFade(1, 0.3f));
            seq.AppendCallback(() => Modal.gameObject.SetActive(true));
            seq.Play();
        }

        private void Hide()
        {
            Showing = false;
            PlayerItems.EquippedItems.Unsubscribe(this);
            PlayerItems.Inventory.Unsubscribe();
            InputHandler.Instance.EnableInput();

            Sequence seq = DOTween.Sequence();
            seq.Append(Modal.DOFade(0, 0.3f));
            seq.AppendCallback(() =>
            {
                BagController.Clear();
                EquipmentController.Clear();

                Modal.gameObject.SetActive(false);
                InputHandler.Instance.EnableInput();
                TimeController.Instance.ResumeTime();
            });
            seq.Play();
        }

        public void OnEquipmentChanged(Dictionary<EquipmentSlot, IEquipment> PlayerEquips)
        {
            EquipmentController.Display(PlayerEquips.ToDictionary(x => x.Key, x => x.Value));
        }

        public void OnInventoryChanged(List<IItem> playerItems)
        {
            BagController.Display(playerItems.ToList());
        }
    }
}
