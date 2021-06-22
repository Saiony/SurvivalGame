namespace Game.Scripts.Controller.Player
{
    public class MoveUpCommand : Command
    {
        public override string Name { get; set; } = "Move Up";

        public override void Execute()
        {
            PlayerController.Instance.Move_Up();
        }
    }
}