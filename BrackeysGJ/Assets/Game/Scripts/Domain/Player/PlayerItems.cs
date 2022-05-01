using System;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Items;

namespace BrackeysGJ.Assets.Game.Scripts.Domain.PlayerItems
{
    public class PlayerItems : IPlayerItems
    {
        public IInventory Inventory { get; private set; }
        public IEquippedItems EquippedItems { get; private set; }

        private PlayerItems()
        {
            Inventory = null;
            EquippedItems = null;
        }

        public PlayerItems(IInventory inventory, IEquippedItems equippedItems) : this()
        {
            SetInventory(inventory);
            SetEquippedItems(equippedItems);
        }

        private void SetInventory(IInventory inventory)
        {
            if (inventory == null)
                throw new InvalidOperationException("Inventory can't be null");

            Inventory = inventory;
        }

        private void SetEquippedItems(IEquippedItems equippedItems)
        {
            if (equippedItems == null)
                throw new InvalidOperationException("EquippedItems can't be null");

            EquippedItems = equippedItems;
        }
    }
}
