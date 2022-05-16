using System;
using Game.Scripts.Controller.Player;
using Game.Scripts.Controller.Player.Commands;

namespace Game.Scripts.Helper
{
    public static class ToolsHelper
    {
        public static Command NewCommand(ToolActions action)
        {
            switch (action)
            {
                case ToolActions.None:
                    return new EmptyCommand();
                case ToolActions.Plow:
                    return new PlowCommand();
                case ToolActions.Water:
                    return new WaterCommand();
                default:
                    throw new InvalidOperationException("Invalid Tool Action");
            }
        }

        public static Command NewCommand(ToolEquipActions equipAction)
        {
            switch (equipAction)
            {
                case ToolEquipActions.None:
                    return new EmptyCommand();
                case ToolEquipActions.StartConstructionMode:
                    return new ConstructionModeCommand();
                default:
                    throw new InvalidOperationException("Invalid Tool Equip Action");
            }
        }

        public static Command NewCommand(ToolUnequipActions unequipAction)
        {
            switch (unequipAction)
            {
                case ToolUnequipActions.None:
                    return new EmptyCommand();
                case ToolUnequipActions.StopConstructionMode:
                    return new StopConstructionModeCommand();
                default:
                    throw new InvalidOperationException("Invalid Tool Unequip Action");
            }
        }
    }

    public enum ToolActions
    {
        Unknown = 0,
        None = 1,
        Plow = 2,
        Water = 3,
    }

    public enum ToolEquipActions
    {
        Unknown = 0,
        None = 1,
        StartConstructionMode = 2,
    }

    public enum ToolUnequipActions
    {
        Unknown = 0,
        None = 1,
        StopConstructionMode = 2,
    }
}