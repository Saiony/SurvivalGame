using System;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Items;
using Game.Scripts.Controller.Player;

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

