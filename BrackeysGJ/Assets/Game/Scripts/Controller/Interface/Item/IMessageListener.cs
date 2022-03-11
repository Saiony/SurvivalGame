using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Items;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Message;

namespace BrackeysGJ.Assets.Game.Scripts.Controller.Interface
{
    public interface IBaseMessageListener<out T> where T : IMessage
    {
    }

    public interface IMessageListener<T> : IBaseMessageListener<T> where T : IMessage
    {
        void OnMessageReceived(T message);
    }
    
    public interface IHpListener : IMessageListener<IHpMessage>
    {
    }

    public interface IStaminaListener : IMessageListener<IStaminaMessage>
    {   
    }

    public interface IFoodLevelListener : IMessageListener<IFoodLevelMessage>
    {
    }
}