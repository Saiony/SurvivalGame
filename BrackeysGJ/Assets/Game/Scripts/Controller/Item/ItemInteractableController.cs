using Game.Scripts.Controller.Dialogue;

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

        protected override void OnPlayerInteract()
        {
            DialogBoxController.Instance.StartDialog(PlayerController.Dialog("asokasod", "aodkaso"));
        }
    }
}