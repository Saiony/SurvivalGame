using System;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
    public class ItemSO : ScriptableObject
    {
        public string Id;
        public string Name;
        public string Description;
        public Sprite Image;
    }
}