using BrackeysGJ.Assets.Game.Scripts.Domain.Items;
using UnityEngine;

namespace Game.Scripts.Domain.Items
{
    public class Misc : Item
    {
        public Misc(string id, string name, string description, Sprite image, int quantity = 1) : base(id, name, description, image, quantity)
        {
        }
    }
}