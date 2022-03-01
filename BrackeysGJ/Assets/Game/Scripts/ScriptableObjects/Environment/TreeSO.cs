using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects.Environment
{
    [CreateAssetMenu(fileName = "Tree", menuName = "ScriptableObjects/Environment/Tree", order = 1)]
    public class TreeSO : DamageableSO
    {
        [Header("Lives")]
        public int TreeLife;
        public int StumpLife;
        public int LogLife;
        public int LogInHalfLife;

        [Header("Damage when falling")]
        public List<int> FallDamages;
        public List<DamageType> FallDamagesType;
    }
}
