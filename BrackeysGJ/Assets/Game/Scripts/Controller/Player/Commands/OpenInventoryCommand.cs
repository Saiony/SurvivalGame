using BrackeysGJ.Assets.Game.Scripts.Controller.Inventory;
using UnityEngine;
namespace Game.Scripts.Controller.Player
{
    public class OpenInventoryCommand : Command
    {
        public override string Name { get; set; } = "Open Inventory";

        public override void Execute()
        {
            InventorySceneController.Instance.Toggle();
        }
    }
}