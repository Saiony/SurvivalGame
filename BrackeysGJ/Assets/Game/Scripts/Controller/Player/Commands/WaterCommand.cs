using UnityEngine;
namespace Game.Scripts.Controller.Player
{
    public class WaterCommand : Command
    {
        public override string Name { get; set; } = "Water";

        public override void Execute(PlayerController actor)
        {
            actor.Water();
        }
    }
}