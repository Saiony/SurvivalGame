using Game.Scripts.Controller.UI;
using UnityEngine;

namespace Game.Scripts.Controller.Player.Commands
{
    public class StopConstructionModeCommand : Command
    {
        public override string Name { get; set; } = "Stop Construction Mode";
        
        public override void Execute()
        {
            HudController.Instance.HideConstructionWindow();
            Debug.Log("StopConstructionModeCommand invoked");
        }
    }
}