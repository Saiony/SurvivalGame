using System.Collections.Generic;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Items;

public interface IEquippedItems
{
    Dictionary<EquipmentSlot, IEquipment> Equipments { get; }

    void AddEquipment(EquipmentSlot slot, IEquipment equipment);
    void Subscribe(IEquipmentListener listener);
    void Unsubscribe(IEquipmentListener listener);
}

public interface IEquipmentListener
{
    void OnEquipmentChanged(Dictionary<EquipmentSlot, IEquipment> PlayerEquips);
}

public enum EquipmentSlot
{
    Unknown = 0,
    Head = 1,
    Torso = 2,
    Legs = 3,
    Feet = 4,
    Right_Hand = 5,
    Left_Hand = 6
}
