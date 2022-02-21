using System.Collections.Generic;
using BrackeysGJ.Assets.Game.Scripts.Controller.Interface;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Message;
using BrackeysGJ.Assets.Game.Scripts.Domain.Message;
using BrackeysGJ.Assets.Game.Scripts.Manager.Interface;
using System.Linq;
using System;

namespace BrackeysGJ.Assets.Game.Scripts.Manager{
    public class MessageManager : IMessageManager
    {
        private IDictionary<IMessage, IList<IMessageListener>> Messages { get; set; }

        public MessageManager()
        {
            Messages = new Dictionary<IMessage, IList<IMessageListener>>();
            Messages.Add(new HpMessage(10), new List<IMessageListener>());
            Messages.Add(new StaminaMessage(10), new List<IMessageListener>());
        }

        public void Subscribe<T>(IMessageListener listener) where T : IMessage
        {
            var messageKeyValue = Messages.FirstOrDefault(x => x.Key.GetType() == typeof(T));
            if(messageKeyValue.Key == null)
                throw new InvalidOperationException("Message not registered");

            messageKeyValue.Value.Add(listener);
        }

        public void Broadcast<T>(T message) where T : IMessage
        {
            
        }
    }
}