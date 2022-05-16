using Game.Scripts.Controller.UI;

namespace Game.Scripts.Controller.Player.Commands
{
    public class OpenSettingsCommand : Command
    {
        public override string Name { get; set; } = "Open Settings";

        public override void Execute()
        {
            SettingsModalController.Instance.Toggle();
        }
    }
}