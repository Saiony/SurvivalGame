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
         public ConstructionStructure(string id, string name, string description, Sprite image, GameObject prefab, int quantity = 1) 
                : base(id, name, description, image, quantity, prefab)
        {
        }
    }
}