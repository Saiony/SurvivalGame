using System;
using Game.Scripts.Controller.Player;
using UnityEngine;

public static class ConsumableHelper
{
    public static Command NewCommand(ConsumableActions actionEnum)
    {
        switch (actionEnum)
        {
            case ConsumableActions.Plant:
                return new PlantCommand();
            default:
                throw new InvalidOperationException("Invalid Consumable Action");
        }
    }
}

public enum ConsumableActions
{
    Unknown = 0,
    Plant = 1
}

