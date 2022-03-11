using System;
using System.Collections.Generic;
using System.Linq;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Items;

public class Inventory : IInventory
{
    public List<IItem> Items { get; set; }
    private IInventoryListener Listener;

    public Inventory()
    {
        Items = new List<IItem>(15);
        for (int i = 0; i < Items.Capacity; i++)
            Items.Add(null);
    }

    public Inventory(List<IItem> items) : this()
    {
        SetItems(items);
    }

    private void SetItems(List<IItem> items)
    {
        if (items == null)
            throw new InvalidOperationException("Items can't be null");

        Items = items.ToList();
    }

    public void AddItem(IItem item)
    {
        //Check if already has item
        var repeatedItem = Items.FirstOrDefault(x => item.Equals(x));
        if (repeatedItem != null)
        {
            repeatedItem.IncrementQuantity(item.Quantity);
            return;
        }

        var firstNullItem = Items.FirstOrDefault(x => x == null);
        var firstNullItemPos = Items.IndexOf(firstNullItem);
        Items[firstNullItemPos] = item;

        Listener?.OnInventoryChanged(Items.ToList());
    }

    public void AddItem(IItem item, int pos)
    {
        //Check if already has item in this pos
        if (Items[pos] != null && Items[pos].Equals(item))
        {
            Items[pos].IncrementQuantity(item.Quantity);
            return;
        }

        Items[pos] = item;
        Listener?.OnInventoryChanged(Items.ToList());
    }

    public void ConsumeItem(IItem item, int pos)
    {
        if(Items[pos] == null)
            return;

        if (!item.DecrementQuantity(1))
            Items[Items.IndexOf(item)] = null;

        Listener?.OnInventoryChanged(Items.ToList());
    }

    public void MoveItem(int posFrom, int posTo)
    {
        if (posFrom == posTo)
            return;

        var aux = Items[posFrom];
        Items[posFrom] = Items[posTo];
        Items[posTo] = aux;

        Listener?.OnInventoryChanged(Items.ToList());
    }

    public void UseQuickItem(int index)
    {
        if (index >= 5 || index < 0)
            throw new InvalidOperationException("Invalid QuickItems index: " + index);

        Items[index].Use();
    }

    public void Subscribe(IInventoryListener listener)
    {
        Listener = listener;
    }

    public void Unsubscribe()
    {
        Listener = null;
    }
}
