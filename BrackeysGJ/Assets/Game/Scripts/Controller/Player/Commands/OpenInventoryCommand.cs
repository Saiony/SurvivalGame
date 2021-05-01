using UnityEngine;
namespace Game.Scripts.Controller.Player
{
    public class OpenInventoryCommand : Command
    {
        public override string Name { get; set; } = "Open Inventory";

        public override void Execute(PlayerController actor)
        {
            Debug.Log("Toggle Settings Modal");
            InventoryDisplayController.Instance.Toggle();
        }
    }
}