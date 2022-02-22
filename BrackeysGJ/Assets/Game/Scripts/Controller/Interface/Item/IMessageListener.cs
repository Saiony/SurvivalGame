using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Items;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Message;

namespace BrackeysGJ.Assets.Game.Scripts.Controller.Interface
{
    public interface IMessageListenerWithOut<out T> where T : IMessage
    {
    }

    public interface IMessageListener<T> : IMessageListenerWithOut<T> where T : IMessage
    {
        void OnMessageReceived(T message);
    }

    public interface IHpListener<T> : IMessageListener<T> where T : IHpMessage
    {
    
    }

    public interface IStaminaListener<T> : IMessageListener<T> where T : IStaminaMessage
    {
        
    }
}