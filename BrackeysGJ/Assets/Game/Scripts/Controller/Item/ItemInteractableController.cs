using Game.Scripts.Controller.Dialog;
using UnityEngine;

namespace Game.Scripts.Controller.Item
{
    public class ItemInteractableController : Item
    {
        protected override void OnRewind() { }
        protected override void OnFoward() { }

        protected override void OnPlayerEnter()
        {
            print("entrou");
        }

        protected override void OnPlayerExit()
        {
            print("saiu");
        }

        protected override void OnPlayerUse()
        {
            TimeChangeController.Instantiate(this);
        }
    }
}