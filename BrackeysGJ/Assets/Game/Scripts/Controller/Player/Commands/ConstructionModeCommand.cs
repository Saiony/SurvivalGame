using Game.Scripts.Controller.UI;
using UnityEngine;

namespace Game.Scripts.Controller.Player.Commands
{
    public class ConstructionModeCommand : Command
    {
        public override string Name { get; set; } = "Construction Mode";
        
        public override void Execute()
        {
            HudController.Instance.ShowConstructionWindow();
            Debug.Log("construction cmd");
        }
    }
}