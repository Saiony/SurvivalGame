namespace Game.Scripts.Controller.Player.Commands
{
    public class SelectQuickItemCommand_1 : Command
    {
        public override string Name { get; set; } = "Select Quick Item -> 1";

        public override void Execute()
        {
            PlayerController.Instance.UseQuickItem(1);
        }
    }

    public class SelectQuickItemCommand_2 : Command
    {
        public override string Name { get; set; } = "Select Quick Item";

        public override void Execute()
        {
            PlayerController.Instance.UseQuickItem(2);
        }
    }

    public class SelectQuickItemCommand_3 : Command
    {
        public override string Name { get; set; } = "Select Quick Item";

        public override void Execute()
        {
            PlayerController.Instance.UseQuickItem(3);
        }
    }

    public class SelectQuickItemCommand_4 : Command
    {
        public override string Name { get; set; } = "Select Quick Item";

        public override void Execute()
        {
            PlayerController.Instance.UseQuickItem(4);
        }
    }

    public class SelectQuickItemCommand_5 : Command
    {
        public override string Name { get; set; } = "Select Quick Item";

        public override void Execute()
        {
            PlayerController.Instance.UseQuickItem(5);
        }
    }
}