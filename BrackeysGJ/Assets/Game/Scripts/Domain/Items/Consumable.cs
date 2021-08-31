using Game.Scripts.Controller.Player;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace BrackeysGJ.Assets.Game.Scripts.Domain.Items
{
    public class Consumable : Item
    {
        public Consumable(string id, string name, string description, Sprite image, ConsumableActions commandEnum, int quantity = 1) : base(id, name, description, image, quantity)
        {
            var command = ConsumableHelper.NewCommand(commandEnum);
            SetCommand(command);
        }
    }
}