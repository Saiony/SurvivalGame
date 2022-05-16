using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Domain.Interface.Items
{
    public interface IEquipment : IItem
    {
        EquipmentSlot Slot { get; }
        GameObject Prefab { get; }
    }
}
