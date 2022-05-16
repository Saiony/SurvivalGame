namespace Game.Scripts.Controller.Player.Commands
{
    public class RunCommand : Command
    {
        public override string Name { get; set; } = "RunCommand";

        public override void Execute()
        {
            PlayerController.Instance.StartRunning();
        }
    }
}