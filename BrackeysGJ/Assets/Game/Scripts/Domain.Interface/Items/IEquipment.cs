using System;
using System.Collections.Generic;
using UnityEngine;

namespace BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Items
{
    public interface IEquipment : IItem
    {
        EquipmentSlot Slot { get; }
        GameObject Prefab { get; }
    }
}
