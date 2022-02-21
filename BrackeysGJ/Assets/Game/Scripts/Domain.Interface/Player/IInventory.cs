using System.Collections.Generic;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Items;

public interface IInventory
{
    List<IItem> Items { get; set; }

    void AddItem(IItem item);
    void AddItem(IItem item, int pos);
    void RemoveItem(IItem item);
    void UseQuickItem(int index);
    void MoveItem(int posFrom, int posTo);

    void Subscribe(IInventoryListener listener);
    void Unsubscribe();
}

public interface IInventoryListener
{
    void OnInventoryChanged(List<IItem> playerItems);
}
