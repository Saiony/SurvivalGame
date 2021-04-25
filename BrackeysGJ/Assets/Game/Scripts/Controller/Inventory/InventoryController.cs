using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private List<Item> QuickItems => Items.Take(5).ToList();
    public List<Item> Items { get; set; }
    private List<InventoryListener> Listeners { get; set; }

    private void Awake()
    {
        Items = new List<Item>(15);
        Listeners = new List<InventoryListener>();

        for (int i = 0; i < Items.Capacity; i++)
            Items.Add(null);
    }

    public bool AddItem(Item item)
    {
        var firstNullItem = Items.FirstOrDefault(x => x == null);
        var firstNullItemPos = Items.IndexOf(firstNullItem);
        Items[firstNullItemPos] = item;

        Listeners.ForEach(x => x.OnInventoryChanged());
        return true;
    }

    public void MoveItem(int posFrom, int posTo)
    {
        if (posFrom == posTo)
            return;

        var aux = Items[posFrom];
        Items[posFrom] = Items[posTo];
        Items[posTo] = aux;

        Listeners.ForEach(x => x.OnInventoryChanged());
    }

    public void Subscribe(InventoryListener listener)
    {
        Listeners.Add(listener);
    }
}

public interface InventoryListener
{
    void OnInventoryChanged();
}
