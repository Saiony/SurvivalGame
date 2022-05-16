using System.Collections.Generic;
using BrackeysGJ.Assets.Game.Scripts.Controller.Inventory;
using Game.Scripts.Domain.Interface.Items;
using UnityEngine;

public class InventoryBagController : MonoBehaviour
{
    [SerializeField]
    private List<BagItemDisplayController> _items = null;
    private List<BagItemDisplayController> Items => _items;

    public void Init(InventoryItemDisplayListener itemListener)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            Items[i].Init(i, itemListener);
        }
    }

    public void Display(IList<IItem> playerItems)
    {
        for (int i = 0; i < playerItems.Count; i++)
        {
            Items[i].DisplayItem(playerItems[i]);
        }
    }
}
