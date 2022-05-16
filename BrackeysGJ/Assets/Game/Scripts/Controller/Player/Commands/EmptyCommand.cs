namespace Game.Scripts.Controller.Player.Commands
{
    public class EmptyCommand : Command
    {
        public override string Name { get; set; } = "Empty";
        
        public override void Execute()
        {
            //Do nothing
        }
    }
}