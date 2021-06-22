using System;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item/Consumable", order = 1)]
    public class ConsumableSO : ItemSO
    {
        public ConsumableActions Command;
    }
}