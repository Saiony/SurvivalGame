using System;
using Game.Scripts.Helper;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item/Tool", order = 1)]
    public class ToolSO : ItemSO
    {
        public ToolActions Command;
        public ToolEquipActions EquipCommand = ToolEquipActions.None;
        public ToolUnequipActions UnequipCommand = ToolUnequipActions.None;
    }
}