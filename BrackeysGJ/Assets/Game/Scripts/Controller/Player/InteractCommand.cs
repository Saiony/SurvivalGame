using UnityEngine;
namespace Game.Scripts.Controller.Player
{
    public class InteractCommand : Command
    {
        public override string Name { get; set; } = "Interact";

        public override void Execute(PlayerController actor)
        {
            actor.Interact();
        }
    }
}