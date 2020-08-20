namespace Game.Scripts.Controller.Player
{
    public class MoveUpCommand : Command
    {
        public override void Execute(PlayerController actor)
        {
            actor.Move_Up();
        }
    }
}