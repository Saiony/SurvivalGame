namespace Game.Scripts.Controller.Player
{
    public class MoveDownCommand : Command
    {
        public override string Name { get; set; } = "Move Down";

        public override void Execute()
        {
            PlayerController.Instance.Move_Down();
        }
    }
}