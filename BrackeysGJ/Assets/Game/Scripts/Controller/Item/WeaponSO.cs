using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Helper;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item/Weapon", order = 1)]
    public class WeaponSO : ItemSO
    {
        public EquipmentSlot Slot;

        public WeaponActions Command;

        [HideInInspector]
        public List<DamageType> DamagesType;

        [HideInInspector]
        public List<int> DamagesValue;

        public WeaponSO()
        {
            Command = WeaponActions.Attack;
            DamagesType = new List<DamageType>();
            DamagesValue = new List<int>();
            Slot = EquipmentSlot.Unknown;
        }

        public void AddDamage()
        {
            DamagesType.Add(DamageType.Unknown);
            DamagesValue.Add(0);
        }

        public void RemoveDamage()
        {
            DamagesType.RemoveAt(DamagesType.Count - 1);
            DamagesValue.RemoveAt(DamagesValue.Count - 1);
        }
    }
}