namespace Game.Scripts.Controller.Player.Commands
{
    public class StopRunningCommand : Command
    {
        public override string Name { get; set; } = "StopRunningCommand";

        public override void Execute()
        {
            PlayerController.Instance.StopRunning();
        }
    }
}