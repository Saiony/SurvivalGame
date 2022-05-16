namespace Game.Scripts.Controller.Player.Commands
{
    public class MoveCommand : Command
    {
        public override string Name { get; set; } = "Move Up";

        public override void Execute()
        {
            PlayerController.Instance.Move();
        }
    }
}