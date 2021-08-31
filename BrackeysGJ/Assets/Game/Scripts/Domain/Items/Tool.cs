using Game.Scripts.Controller.Player;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace BrackeysGJ.Assets.Game.Scripts.Domain.Items
{
    public class Tool : Item
    {
        public Tool(string id, string name, string description, Sprite image, ToolActions commandEnum, int quantity = 1) : base(id, name, description, image, quantity)
        {
            var command = ToolsHelper.NewCommand(commandEnum);
            SetCommand(command);
        }
    }
}