using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private List<Item> QuickItems { get; set; }
    public List<Item> Items { get; set; }

    private void Start()
    {
        QuickItems = new List<Item>(8);
        Items = new List<Item>(15);
    }

    public bool AddItem(Item item)
    {
        if (Items.Count >= Items.Capacity)
            return false;

        Items.Add(item);
        return true;
    }
}
