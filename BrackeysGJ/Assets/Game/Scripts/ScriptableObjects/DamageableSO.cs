using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    public class DamageableSO : ScriptableObject
    {
        [HideInInspector]
        public List<DamageType> DamagesType;

        [HideInInspector]
        public List<int> DamageMultiplier;

        public DamageableSO()
        {
            DamagesType = new List<DamageType>();
            DamageMultiplier = new List<int>();
        }

        public void AddResistance()
        {
            DamagesType.Add(DamageType.Unknown);
            DamageMultiplier.Add(0);
        }

        public void RemoveResistance()
        {
            DamagesType.RemoveAt(DamagesType.Count - 1);
            DamageMultiplier.RemoveAt(DamageMultiplier.Count - 1);
        }
    }
}
