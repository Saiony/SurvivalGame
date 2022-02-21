using BrackeysGJ.Assets.Game.Scripts.Controller.Interface;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Message;

namespace BrackeysGJ.Assets.Game.Scripts.Manager.Interface
{
    public interface IMessageManager : IBaseManager
    {
        void Subscribe<T>(IMessageListener listener) where T : IMessage;    
        void Broadcast<T>(T message) where T : IMessage;
    }
}