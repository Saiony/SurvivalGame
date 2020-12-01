namespace Game.Scripts.Controller.Player
{
    public class MoveLeftCommand : Command
    {
        public override string Name { get; set; } = "Move Left";

        public override void Execute(PlayerController actor)
        {
            actor.Move_Left();
        }
    }
}