using UnityEngine;
namespace Game.Scripts.Controller.Player
{
    public class PlantCommand : Command
    {
        public override string Name { get; set; } = "Plant";

        public override void Execute(PlayerController actor)
        {
            actor.PlayPlantAnimation();
        }
    }
}