using System;
using System.Collections.Generic;

namespace BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Items
{
    public interface IEquipment : IItem
    {
        EquipmentSlot Slot { get; }
    }
}
