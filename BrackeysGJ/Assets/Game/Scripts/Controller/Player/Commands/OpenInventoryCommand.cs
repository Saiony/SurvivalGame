using BrackeysGJ.Assets.Game.Scripts.Controller.Inventory;

namespace Game.Scripts.Controller.Player.Commands
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