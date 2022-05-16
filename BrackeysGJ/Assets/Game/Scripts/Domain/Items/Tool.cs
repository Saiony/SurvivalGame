using System;
using Game.Scripts.Controller.Player;
using Game.Scripts.Controller.Player.Commands;
using Game.Scripts.Domain.Interface.Items;
using Game.Scripts.Helper;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.Domain.Items
{
    public class Tool : Equipment, ITool
    {
        public Command ToolEquipAction { get; private set;  }
        public Command ToolUnequipAction { get; private set;  }

        public Tool(string id, string name, string description, Sprite image, ToolActions toolAction, 
                    ToolEquipActions toolEquipAction, ToolUnequipActions toolUnequipAction, GameObject prefab, int quantity = 1) 
                    : base(id, name, description, image, EquipmentSlot.Right_Hand, quantity, prefab)
        {
            var command = ToolsHelper.NewCommand(toolAction);
            SetCommand(command);
            SetToolEquipAction(toolEquipAction);
            SetToolUnequipAction(toolUnequipAction);
        }

        private void SetToolEquipAction(ToolEquipActions toolEquipAction)
        {
            if (toolEquipAction == ToolEquipActions.Unknown)
                throw new InvalidOperationException("ToolEquipAction can't be Unknown");

            ToolEquipAction = ToolsHelper.NewCommand(toolEquipAction);
        }

        private void SetToolUnequipAction(ToolUnequipActions toolUnequipAction)
        {
            if (toolUnequipAction == ToolUnequipActions.Unknown)
                throw new InvalidOperationException("ToolUnequipAction can't be Unknown");

            ToolUnequipAction = ToolsHelper.NewCommand(toolUnequipAction);
        }
    }
}