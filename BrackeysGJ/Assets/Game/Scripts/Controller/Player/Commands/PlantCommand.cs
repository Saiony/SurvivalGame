namespace Game.Scripts.Controller.Player.Commands
{
    public class PlantCommand : Command
    {
        public override string Name { get; set; } = "Plant";

        public override void Execute()
        {
            PlayerController.Instance.PlayPlantAnimation();
        }
    }
}