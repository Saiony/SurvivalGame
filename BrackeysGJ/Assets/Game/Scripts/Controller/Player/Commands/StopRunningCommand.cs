namespace Game.Scripts.Controller.Player
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