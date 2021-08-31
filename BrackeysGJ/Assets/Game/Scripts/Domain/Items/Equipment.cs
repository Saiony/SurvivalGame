using System;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Items;
using UnityEngine;

namespace BrackeysGJ.Assets.Game.Scripts.Domain.Items
{
    public abstract class Equipment : Item, IEquipment
    {
        public EquipmentSlot Slot { get; private set; }

        public Equipment(string id, string name, string description, Sprite image, EquipmentSlot slot, int quantity) : base(id, name, description, image, quantity)
        {
            SetSlot(slot);
        }

        private void SetSlot(EquipmentSlot slot)
        {
            if (slot == EquipmentSlot.Unknown)
                throw new InvalidOperationException("Slot can't be Unknown");

            Slot = slot;
        }
    }
}
