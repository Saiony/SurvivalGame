using System;
using System.Collections.Generic;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Items;
using System.Linq;
using UnityEngine;
using BrackeysGJ.Assets.Game.Scripts.Domain.Items;

namespace BrackeysGJ.Assets.Game.Scripts.Domain.PlayerItems
{
    public class EquippedItems : IEquippedItems
    {
        public Dictionary<EquipmentSlot, IEquipment> Equipments { get; private set; }
        private IEquipmentListener Listener { get; set; }

        public EquippedItems()
        {
            Equipments = new Dictionary<EquipmentSlot, IEquipment>();
            Equipments.Add(EquipmentSlot.Head, null);
            Equipments.Add(EquipmentSlot.Torso, null);
            Equipments.Add(EquipmentSlot.Legs, null);
            Equipments.Add(EquipmentSlot.Feet, null);
            Equipments.Add(EquipmentSlot.Right_Hand, null);
            Equipments.Add(EquipmentSlot.Left_Hand, null);
        }

        public EquippedItems(IDictionary<EquipmentSlot, IEquipment> equipments) : this()
        {
            SetEquipment(equipments);
        }

        private void SetEquipment(IDictionary<EquipmentSlot, IEquipment> equipments)
        {
            if (equipments == null)
                return;

            foreach (var equipment in equipments)
                Equipments[equipment.Key] = equipment.Value;
        }

        public void AddEquipment(EquipmentSlot slot, IEquipment equipment)
        {
            if (equipment != null && slot != equipment.Slot)
                throw new InvalidOperationException("gandhi disse que nao pode");

            Equipments[slot] = equipment;
            Listener?.OnEquipmentChanged(Equipments);
        }

        public void Subscribe(IEquipmentListener listener)
        {
            Listener = listener;
        }

        public void Unsubscribe()
        {
            Listener = null;
        }
    }
}
