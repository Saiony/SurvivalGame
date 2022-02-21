namespace BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Message
{
    public interface IMessage
    {
         
    }

    public interface IHpMessage : IMessage
    {
        int Hp { get; }
    }

    public interface IStaminaMessage : IMessage
    {
        int Stamina { get; }
    }
}