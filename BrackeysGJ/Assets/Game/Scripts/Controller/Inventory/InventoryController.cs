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

        for (int i = 0; i < Items.Capacity; i++)
            Items.Add(null);
    }

    public bool AddItem(Item item)
    {
        var firstNullItem = Items.FirstOrDefault(x => x == null);
        var firstNullItemPos = Items.IndexOf(firstNullItem);
        Items[firstNullItemPos] = item;

        return true;
    }
}
