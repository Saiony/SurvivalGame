using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrackeysGJ.Assets.Game.Scripts.Domain.Items;
using UnityEngine;

namespace Game.Scripts.Domain.Items
{
    public class ConstructionStructure : Item
    {
        int _rotations { get; set; }

        public Vector3Int Size { get; private set; }

        public ConstructionStructure(string id, string name, string description, Sprite image, GameObject prefab, Vector3Int size, int quantity = 1)
               : base(id, name, description, image, quantity, prefab)
        {
            SetSize(size);
        }

        void SetSize(Vector3Int size)
        {
            Size = size;
        }
    }
}