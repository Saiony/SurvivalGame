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

    public void SelectQuickItem(int index)
    {
        QuickItemsController.SelectItem(index);
    }
}

public interface InventoryListener
{
    void OnInventoryChanged();
}
