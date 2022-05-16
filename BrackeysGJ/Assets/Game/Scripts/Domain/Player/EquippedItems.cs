using System;
using System.Collections.Generic;
using Game.Scripts.Domain.Interface.Items;

namespace BrackeysGJ.Assets.Game.Scripts.Domain.PlayerItems
{
    public class EquippedItems : IEquippedItems
    {
        public Dictionary<EquipmentSlot, IEquipment> Equipments { get; private set; }
        private List<IEquipmentListener> Listeners { get; set; }

        public EquippedItems()
        {
            Listeners = new List<IEquipmentListener>();
            
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

            if(Equipments[slot] != null && Equipments[slot] is ITool previousTool)
                previousTool.ToolUnequipAction.Execute();
                
            Equipments[slot] = equipment;
            Listeners.ForEach(x => x.OnEquipmentChanged(Equipments));
            
            if(equipment is ITool tool)
                tool.ToolEquipAction.Execute();
        }

        public void Subscribe(IEquipmentListener listener)
        {
            Listeners.Add(listener);
        }

        public void Unsubscribe(IEquipmentListener listener)
        {
            Listeners.Remove(listener);
        }
    }
}
