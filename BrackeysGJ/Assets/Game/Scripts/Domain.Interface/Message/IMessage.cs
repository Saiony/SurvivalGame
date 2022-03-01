using BrackeysGJ.Assets.Game.Scripts.Domain.Player;

namespace BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Message
{
    public interface IMessage
    {
         
    }

    public interface IHpMessage : IMessage
    {
        Hp Hp { get; }
    }

    public interface IStaminaMessage : IMessage
    {
        Stamina Stamina { get; }
    }
}