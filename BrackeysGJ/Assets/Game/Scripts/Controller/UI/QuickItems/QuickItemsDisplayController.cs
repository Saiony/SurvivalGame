using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Controller.Player;
using UnityEngine;

public class QuickItemsDisplayController : MonoBehaviour, InventoryListener
{
    [SerializeField]
    private InventoryController _inventory = null;
    private InventoryController Inventory => _inventory;

    [SerializeField]
    private List<QuickItemDisplayController> _quickItems = null;
    private List<QuickItemDisplayController> QuickItems => _quickItems;

    void Start()
    {
        Refresh();
        Inventory.Subscribe(this);
    }

    private void Refresh()
    {
        for (int i = 0; i < QuickItems.Count; i++)
        {
            QuickItems[i].SetItem(Inventory.Items[i]);
        }
    }

    public void OnInventoryChanged()
    {
        Refresh();
    }
}
