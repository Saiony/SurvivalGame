using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Items;
public interface IPlayerItems
{
    IInventory Inventory { get; }
    IEquippedItems EquippedItems { get; }

    // void Equip(IEquipment equipment);
    // void Unequip(EquipmentSlot equipmentPosition);]
}

