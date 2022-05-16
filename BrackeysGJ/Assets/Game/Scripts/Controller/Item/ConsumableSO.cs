using System;
using Game.Scripts.Helper;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item/Consumable", order = 1)]
    public class ConsumableSO : ItemSO
    {
        public int HungerSatisfied;
        public int HealthGiven;
        public ConsumableActions Command;
    }
}