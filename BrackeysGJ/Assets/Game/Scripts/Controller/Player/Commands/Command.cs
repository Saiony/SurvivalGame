namespace Game.Scripts.Controller.Player.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; set; }
        public abstract void Execute();
    }
}