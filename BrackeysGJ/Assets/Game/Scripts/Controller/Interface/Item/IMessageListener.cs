using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Message;

namespace BrackeysGJ.Assets.Game.Scripts.Controller.Interface
{
    public interface IMessageListener<T> where T : IMessage
    {
        void OnMessageReceived(T message);
    }

    public interface IHpListener<IHpMessage> : IMessageListener<IHpMessage>
    {
    
    }

    // public interface IStaminaListener<IStaminaMessage> : IMessageListener<IStaminaMessage>
    // {
        
    // }
}