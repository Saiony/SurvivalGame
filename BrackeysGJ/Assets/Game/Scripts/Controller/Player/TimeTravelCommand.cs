namespace Game.Scripts.Controller.Player
{
    public class TimeTravelCommand : Command
    {
        public override void Execute(PlayerController playerController)
        {
            playerController.TimeTravel();
        }
    }
}