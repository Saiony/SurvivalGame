namespace Game.Scripts.Controller.Player.Commands
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