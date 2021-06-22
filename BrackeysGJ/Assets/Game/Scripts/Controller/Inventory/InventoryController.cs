using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    [SerializeField]
    private QuickItemsDisplayController _quickItemsController = null;
    private QuickItemsDisplayController QuickItemsController => _quickItemsController;

    public List<Item> Items { get; set; }
    private List<InventoryListener> Listeners { get; set; }
    public Item SelectedItem => QuickItemsController.SelectedItem.Item;

    private void Awake()
    {
        Items = new List<Item>(15);
        Listeners = new List<InventoryListener>();

        for (int i = 0; i < Items.Capacity; i++)
            Items.Add(null);
    }

    public void AddItem(Item item)
    {
        //Check if already has item
        var repeatedItem = Items.FirstOrDefault(x => item.Equals(x));
        if (repeatedItem != null)
        {
            repeatedItem.IncrementQuantity(item.Quantity);
            NotifyListener();
            return;
        }

        var firstNullItem = Items.FirstOrDefault(x => x == null);
        var firstNullItemPos = Items.IndexOf(firstNullItem);
        Items[firstNullItemPos] = item;

        NotifyListener();
    }

    public void RemoveItem(Item item)
    {
        //Check if already has item
        var repeatedItem = Items.FirstOrDefault(x => item.Equals(x));
        if (repeatedItem == null)
            return;

        if (!repeatedItem.DecrementQuantity(1))
            Items[Items.IndexOf(repeatedItem)] = null;

        NotifyListener();
    }

    public void MoveItem(int posFrom, int posTo)
    {
        if (posFrom == posTo)
            return;

        var aux = Items[posFrom];
        Items[posFrom] = Items[posTo];
        Items[posTo] = aux;

        NotifyListener();
    }

    public void Subscribe(InventoryListener listener)
    {
        Listeners.Add(listener);
    }

    public void SelectQuickItem(int index)
    {
        QuickItemsController.SelectItem(index);
    }

    public void UseSelectedItem()
    {
        if (SelectedItem == null)
            return;

        SelectedItem.Use();
        if (SelectedItem is Consumable)
            RemoveItem(SelectedItem);
    }

    private void NotifyListener()
    {
        Listeners.ForEach(x => x.OnInventoryChanged());
    }
}

public interface InventoryListener
{
    void OnInventoryChanged();
}
