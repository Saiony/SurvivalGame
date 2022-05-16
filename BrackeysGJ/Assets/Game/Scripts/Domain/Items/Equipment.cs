using System;
using Game.Scripts.Domain.Interface.Items;
using UnityEngine;

namespace Game.Scripts.Domain.Items
{
    public abstract class Equipment : Item, IEquipment
    {
        public EquipmentSlot Slot { get; private set; }
        public GameObject Prefab { get; private set; }

        public Equipment(string id, string name, string description, Sprite image, EquipmentSlot slot, int quantity, GameObject prefab) 
                         : base(id, name, description, image, quantity)
        {
            SetSlot(slot);
            SetPrefab(prefab);
        }

        private void SetSlot(EquipmentSlot slot)
        {
            if (slot == EquipmentSlot.Unknown)
                throw new InvalidOperationException("Slot can't be Unknown");

            Slot = slot;
        }

        private void SetPrefab(GameObject prefab)
        {
            if(prefab == null)
                throw new InvalidOperationException("Prefab can't be null");

            Prefab = prefab;
        }
    }
}
