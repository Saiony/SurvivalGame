using UnityEngine;
namespace Game.Scripts.Controller.Player
{
    public class ChopCommand : Command
    {
        public override string Name { get; set; } = "Chop";

        public override void Execute()
        {
            PlayerController.Instance.PlayChopAnimation();
        }
    }
}