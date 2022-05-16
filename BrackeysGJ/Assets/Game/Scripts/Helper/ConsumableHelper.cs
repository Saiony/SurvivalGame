using System;
using Game.Scripts.Controller.Player;
using Game.Scripts.Controller.Player.Commands;
using Game.Scripts.Domain.Interface.Items;

namespace Game.Scripts.Helper
{
    public static class ConsumableHelper
    {
        public static Command NewCommand(ConsumableActions actionEnum, IConsumable consumable)
        {
            switch (actionEnum)
            {
                case ConsumableActions.Plant:
                    return new PlantCommand();
                case ConsumableActions.Eat:
                    return new EatCommand(consumable);
                default:
                    throw new InvalidOperationException("Invalid Consumable Action");
            }
        }
    }

    public enum ConsumableActions
    {
        Unknown = 0,
        Plant,
        Eat,
    }
}