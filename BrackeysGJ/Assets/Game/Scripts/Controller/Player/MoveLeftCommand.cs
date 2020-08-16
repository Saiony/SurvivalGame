namespace Game.Scripts.Controller.Player
{
    public class MoveLeftCommand : Command
    {
        public override void Execute(PlayerController actor)
        {
            actor.Move_Left();
        }
    }
}