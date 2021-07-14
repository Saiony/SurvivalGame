using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item/Weapon", order = 1)]
    public class WeaponSO : ItemSO
    {
        public WeaponActions Command;

        [HideInInspector]
        public List<AttackType> AttackTypes;

        [HideInInspector]
        public List<int> AttackDamages;

        public WeaponSO()
        {
            AttackTypes = new List<AttackType>();
            AttackDamages = new List<int>();
        }

        public void AddDamage()
        {
            AttackTypes.Add(AttackType.Unknown);
            AttackDamages.Add(0);
        }

        public void RemoveDamage()
        {
            AttackTypes.RemoveAt(AttackTypes.Count - 1);
            AttackDamages.RemoveAt(AttackDamages.Count - 1);
        }
    }
}