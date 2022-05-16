namespace Game.Scripts.Controller.Player.Commands
{
    public class InteractCommand : Command
    {
        public override string Name { get; set; } = "Interact";

        public override void Execute()
        {
            PlayerController.Instance.Interact();
        }
    }
}