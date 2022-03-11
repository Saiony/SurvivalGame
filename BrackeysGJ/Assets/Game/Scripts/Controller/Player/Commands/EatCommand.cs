using System;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Items;

namespace Game.Scripts.Controller.Player
{
    public class EatCommand : Command
    {
        public override string Name { get; set; } = "Eat";
        private IConsumable Consumable;

        public EatCommand(IConsumable consumable)
        {
            if(consumable == null)  
                throw new InvalidOperationException("Consumable can't be null");

            Consumable = consumable;
        }

        public override void Execute()
        {
           PlayerController.Instance.Eat(Consumable);
        }
    }
}