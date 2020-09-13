using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Controller.UI;
using UnityEngine;
namespace Game.Scripts.Controller.Player
{
    public class OpenSettingsCommand : Command
    {
        public override string Name { get; set; } = "Open Settings";

        public override void Execute(PlayerController actor)
        {
            Debug.Log("Toggle Settings Modal");
            SettingsModalController.Instance.Toggle();
        }
    }
}