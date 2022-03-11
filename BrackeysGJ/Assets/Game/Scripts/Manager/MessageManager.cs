using System.Collections.Generic;
using BrackeysGJ.Assets.Game.Scripts.Controller.Interface;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Message;
using BrackeysGJ.Assets.Game.Scripts.Domain.Message;
using BrackeysGJ.Assets.Game.Scripts.Manager.Interface;
using System.Linq;
using System;
using BrackeysGJ.Assets.Game.Scripts.Domain.Player;

namespace BrackeysGJ.Assets.Game.Scripts.Manager{
    public class MessageManager : IMessageManager
    {
        private IDictionary<IMessage, IList<IBaseMessageListener<IMessage>>> Listeners { get; set; }
        private List<IBaseMessageListener<IMessage>> doce { get; set; }

        public MessageManager()
        {
            Listeners = new Dictionary<IMessage, IList<IBaseMessageListener<IMessage>>>();
            Listeners.Add(new HpMessage(new Hp(0, 1)), new List<IBaseMessageListener<IMessage>>());
            Listeners.Add(new StaminaMessage(new Stamina(0, 0)), new List<IBaseMessageListener<IMessage>>());
            Listeners.Add(new FoodLevelMessage(new FoodLevel(0, 1, new ScriptableObjects.Player.FoodLevelSO())), new List<IBaseMessageListener<IMessage>>());
        }

        public void Subscribe<T>(IMessageListener<T> listener) where T : IMessage
        {
            var messageKeyValue = Listeners.FirstOrDefault(x => x.Key is T);
            if(messageKeyValue.Key == null)
                throw new InvalidOperationException("Message not registered");

            messageKeyValue.Value.Add((IBaseMessageListener<IMessage>) listener);     
        }

        public void Broadcast<T>(T message) where T : IMessage
        {
            var messageKeyValue = Listeners.FirstOrDefault(x => x.Key is T);
            if(messageKeyValue.Key == null)
                return;
            
            messageKeyValue.Value.ToList().ForEach(x => ((IMessageListener<T>) x).OnMessageReceived(message));
        }
    }
}