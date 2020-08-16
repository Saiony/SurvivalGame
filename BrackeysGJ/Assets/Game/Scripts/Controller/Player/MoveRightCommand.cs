namespace Game.Scripts.Controller.Player
{
    public class MoveRightCommand : Command
    {
        public override void Execute(PlayerController actor)
        {
            actor.Move_Right();
        }
    }
}