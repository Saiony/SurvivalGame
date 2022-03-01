using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    public class DamageableSO : ScriptableObject
    {
        [HideInInspector]
        public List<DamageType> DamagesTakenType;

        [HideInInspector]
        public List<int> DamagesTakenMultiplier;

        public DamageableSO()
        {
            DamagesTakenType = new List<DamageType>();
            DamagesTakenMultiplier = new List<int>();
        }

        public void AddResistance()
        {
            DamagesTakenType.Add(DamageType.Unknown);
            DamagesTakenMultiplier.Add(0);
        }

        public void RemoveResistance()
        {
            DamagesTakenType.RemoveAt(DamagesTakenType.Count - 1);
            DamagesTakenMultiplier.RemoveAt(DamagesTakenMultiplier.Count - 1);
        }
    }
}
