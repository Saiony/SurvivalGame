using BrackeysGJ.Assets.Game.Scripts.Controller.Interface;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Message;
using BrackeysGJ.Assets.Game.Scripts.Manager.Interface;

namespace BrackeysGJ.Assets.Game.Scripts.Manager{
    public class MessageManager : IMessageManager
    {
        public void Subscribe<T>(IMessageListener listener) where T : IMessage
        {
            
        }
    }
}