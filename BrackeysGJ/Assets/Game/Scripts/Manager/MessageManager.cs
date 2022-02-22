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
        private IDictionary<IMessage, IList<IMessageListenerWithOut<IMessage>>> Messages { get; set; }

        public MessageManager()
        {
            Messages = new Dictionary<IMessage, IList<IMessageListenerWithOut<IMessage>>>();
            Messages.Add(new HpMessage(10), new List<IMessageListenerWithOut<IMessage>>());
            Messages.Add(new StaminaMessage(10), new List<IMessageListenerWithOut<IMessage>>());
        }

        public void Subscribe<T>(IMessageListener<T> listener) where T : IMessage
        {
            var messageKeyValue = Messages.FirstOrDefault(x => x.Key is T);
            if(messageKeyValue.Key == null)
                throw new InvalidOperationException("Message not registered");


            var listenerConverted = (IMessageListenerWithOut<IMessage>) listener;
            messageKeyValue.Value.Add(listenerConverted);
        }

        public void Broadcast<T>(T message) where T : IMessage
        {
            var messageKeyValue = Messages.FirstOrDefault(x => x.Key is T);
            if(messageKeyValue.Key == null)
                throw new InvalidOperationException("Message not registered");
            
            messageKeyValue.Value.ToList().ForEach(x => ((IMessageListener<IMessage>) x).OnMessageReceived(message));
        }
    }
}