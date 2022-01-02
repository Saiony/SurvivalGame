namespace Game.Scripts.Controller.Player
{
    public class RightArmCommand : Command
    {
        public override string Name { get; set; } = "Right Arm";

        public override void Execute()
        {
            PlayerController.Instance.UseRightArmItem();
        }
    }
}