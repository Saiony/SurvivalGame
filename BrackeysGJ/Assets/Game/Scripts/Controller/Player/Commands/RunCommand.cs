using Game.Scripts.Controller.Player;

namespace Game.Scripts.Controller.Player
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