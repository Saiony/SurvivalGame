using System;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item/Tool", order = 1)]
    public class ToolSO : ItemSO
    {
        public ToolActions Command;
    }
}