namespace Game.Scripts.Controller.Player
{
    public class TimeTravelCommand : Command
    {
        public override string Name { get; set; } = "Time Travel";

        public override void Execute(PlayerController playerController)
        {
            playerController.TimeTravel();
        }
    }
}