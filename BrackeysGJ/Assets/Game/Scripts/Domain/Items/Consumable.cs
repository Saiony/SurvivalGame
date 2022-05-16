using Game.Scripts.Domain.Interface.Items;
using Game.Scripts.Domain.Items;
using Game.Scripts.Helper;
using UnityEngine;

namespace BrackeysGJ.Assets.Game.Scripts.Domain.Items
{
    public class Consumable : Item, IConsumable
    {
        public int HungerSatisfied { get; private set; }
        public int HealthGiven { get; private set; }

        public Consumable(string id, string name, string description, Sprite image, ConsumableActions commandEnum,
                          int hungerSatisfied, int healthGiven, int quantity = 1) 
                          : base(id, name, description, image, quantity)
        {
            HungerSatisfied = hungerSatisfied;
            HealthGiven = healthGiven;
            
            var command = ConsumableHelper.NewCommand(commandEnum, this);
            SetCommand(command);
        }

    }
}