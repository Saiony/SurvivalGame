namespace Game.Scripts.Controller.Player
{
    public abstract class Command
    {
        public abstract void Execute(PlayerController actor);
    }
}