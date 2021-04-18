using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class InventoryDisplayController : MonoBehaviour, InventoryItemDisplayListener
{
    [SerializeField]
    private CanvasGroup _modal = null;
    private CanvasGroup Modal => _modal;

    [SerializeField]
    private InventoryController _inventory = null;
    private InventoryController Inventory => _inventory;

    [SerializeField]
    private InventoryInfoController _inventoryInfo = null;
    private InventoryInfoController InventoryInfo => _inventoryInfo;

    [SerializeField]
    private List<InventoryItemDisplayController> _quickItems = null;
    private List<InventoryItemDisplayController> QuickItems => _quickItems;

    [SerializeField]
    private List<InventoryItemDisplayController> _displayItems = null;
    private List<InventoryItemDisplayController> DisplayItems => _displayItems;

    [SerializeField]
    private ImageFollowMouse _imageFollowMose = null;
    private ImageFollowMouse ImageFollowMouse => _imageFollowMose;

    private bool Showing { get; set; }
    private InventoryItemDisplayController SelectedItem { get; set; }
    public static InventoryDisplayController Instance = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        Modal.gameObject.SetActive(false);
        SelectedItem = null;

        DisplayItems.ForEach(item => item.Init(this));
    }

    public void Toggle()
    {
        if (Showing)
            Hide();
        else
            Show();
    }

    private void Hide()
    {
        Showing = false;
        Sequence seq = DOTween.Sequence();

        seq.Append(Modal.DOFade(0, 0.3f));
        seq.AppendCallback(() => Modal.gameObject.SetActive(false));
        seq.Play();
    }

    private void Show()
    {
        Showing = true;
        Clear();
        Modal.gameObject.SetActive(true);
        Sequence seq = DOTween.Sequence();

        seq.AppendCallback(() => Modal.DOFade(1, 0.3f));
        seq.Play();

        var items = Inventory.Items;
        for (int i = 0; i < items.Count; i++)
        {
            DisplayItems[i].SetItem(items[i]);
        }
    }

    private void Clear()
    {
        QuickItems.ForEach(item => item.Clear());
        DisplayItems.ForEach(item => item.Clear());
    }

    public void OnItemDisplayClicked(InventoryItemDisplayController itemDisplay)
    {
        if (SelectedItem != null)
        {
            SwapItems(SelectedItem, itemDisplay);
            DeselectItem(itemDisplay);
            return;
        }
        if (itemDisplay.Item == null)
            return;

        SelectItem(itemDisplay);
    }

    private void SelectItem(InventoryItemDisplayController item)
    {
        SelectedItem = item;
        item.Select();

        InventoryInfo.DisplayItem(item.Item);
        ImageFollowMouse.Show(SelectedItem.Item.Image);
    }

    private void DeselectItem(InventoryItemDisplayController item)
    {
        ImageFollowMouse.Hide();

        SelectedItem = null;
        item.Deselect();
    }

    private void SwapItems(InventoryItemDisplayController item1, InventoryItemDisplayController item2)
    {
        //change position in list
        var items = Inventory.Items;
        var pos1 = DisplayItems.FindIndex(x => x == item1);
        var pos2 = DisplayItems.FindIndex(x => x == item2);

        var aux = item1.Item;
        items[pos1] = item2.Item;
        items[pos2] = item1.Item;

        //change visually
        DisplayItems[pos1].SetItem(item2.Item);
        DisplayItems[pos2].SetItem(aux);
    }
}
