using Game.Scripts.Controller.UI;
using UnityEngine;
namespace Game.Scripts.Controller.Player
{
    public class UseSelectedItemCommand : Command
    {
        public override string Name { get; set; } = "Use Selected Item";

        public override void Execute()
        {
            Debug.Log($"Cmd: {Name}");
            PlayerController.Instance.UseSelectedItem();
        }
    }
}