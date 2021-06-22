using Game.Scripts.Controller.UI;
using UnityEngine;
namespace Game.Scripts.Controller.Player
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