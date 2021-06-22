using UnityEngine;
namespace Game.Scripts.Controller.Player
{
    public class PlowCommand : Command
    {
        public override string Name { get; set; } = "Plow";

        public override void Execute()
        {
            PlayerController.Instance.PlayPlowAnimation();
        }
    }
}