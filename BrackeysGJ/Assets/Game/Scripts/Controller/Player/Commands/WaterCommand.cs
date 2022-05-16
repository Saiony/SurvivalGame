namespace Game.Scripts.Controller.Player.Commands
{
    public class WaterCommand : Command
    {
        public override string Name { get; set; } = "Water";

        public override void Execute()
        {
            PlayerController.Instance.PlayWaterAnimation();
        }
    }
}