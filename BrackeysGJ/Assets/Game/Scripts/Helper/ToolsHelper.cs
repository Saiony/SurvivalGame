using System;
using Game.Scripts.Controller.Player;
using UnityEngine;

public static class ToolsHelper
{
    public static Command NewCommand(ToolActions actionEnum)
    {
        switch (actionEnum)
        {
            case ToolActions.Plow:
                return new PlowCommand();
            case ToolActions.Water:
                return new WaterCommand();
            default:
                throw new InvalidOperationException("Invalid Tool Action");
        }
    }
}

public enum ToolActions
{
    Unknown = 0,
    Plow = 1,
    Water = 2,
    Attack = 3
}

