namespace Game.Scripts.Controller.Player
{
    public class MoveDownCommand : Command
    {
        public override void Execute(PlayerController actor)
        {
            actor.Move_Down();
        }
    }
}