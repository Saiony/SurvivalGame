using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Controller.Player;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class QuickItemsDisplayController : MonoBehaviour
{
    [SerializeField]
    private List<QuickItemDisplayController> _quickItems = null;
    private List<QuickItemDisplayController> QuickItems => _quickItems;

    public QuickItemDisplayController SelectedItem { get; set; }

    void Start()
    {
        Refresh();
        SelectItem(1);
    }

    private void Refresh()
    {
        for (int i = 0; i < QuickItems.Count; i++)
        {
            QuickItems[i].SetItem(PlayerController.Instance.Items.Inventory.Items[i]);
        }
    }

    public void OnInventoryChanged()
    {
        Refresh();
    }

    public void SelectItem(int index)
    {
        index--;
        if (index < 0 || index > QuickItems.Count)
            throw new InvalidOperationException("Invalid index: " + index);

        SelectedItem?.Deselect();
        SelectedItem = QuickItems[index];
        SelectedItem.Select();
    }
}
