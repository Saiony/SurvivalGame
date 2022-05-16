using System;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    public class ItemSO : ScriptableObject
    {
        public string Id;
        public string Name;
        [TextArea]
        public string Description;
        public Sprite Image;
        public GameObject Prefab;
    }
}