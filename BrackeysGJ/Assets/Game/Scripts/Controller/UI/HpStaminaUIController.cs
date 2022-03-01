using BrackeysGJ.Assets.Game.Scripts.Controller.Interface;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Items;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Message;
using BrackeysGJ.Assets.Game.Scripts.Manager;
using BrackeysGJ.Assets.Game.Scripts.Manager.Interface;
using Game.Scripts.Controller.Player;
using UnityEngine;

namespace BrackeysGJ.Assets.Game.Scripts.Controller.UI
{
    public class HpStaminaUIController : MonoBehaviour, IHpListener, IStaminaListener
    {
        [SerializeField]
        private FillBarController HpBar;
        
        [SerializeField]
        private FillBarController StaminaBar;

        public void Init()
        {
            var playerStats = PlayerController.Instance.Stats;
            HpBar.Init(playerStats.Hp.Current, playerStats.Hp.Max);
            StaminaBar.Init(playerStats.Stamina.Current, playerStats.Stamina.Max);

            var messageManager = ManagerProvider.Instance.Get<IMessageManager>();
            messageManager.Subscribe<IHpMessage>(this);
            messageManager.Subscribe<IStaminaMessage>(this);
        }

        public void OnMessageReceived(IStaminaMessage message)
        {
            StaminaBar.UpdateValue(message.Stamina.Current);
        }
        public void OnMessageReceived(IHpMessage message)
        {
            HpBar.UpdateValue(message.Hp.Current);
        }
    }
}