using UnityEngine;
namespace Game.Scripts.Controller.Player
{
    public class InteractCommand : Command
    {
        public override void Execute(PlayerController actor)
        {
            actor.Interact();
        }
    }
}