using BrackeysGJ.Assets.Game.Scripts.Controller.Interface;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Message;
using BrackeysGJ.Assets.Game.Scripts.Manager;
using BrackeysGJ.Assets.Game.Scripts.Manager.Interface;
using Game.Scripts.Controller.Player;
using UnityEngine;

namespace Game.Scripts.Controller.UI
{
    public class PlayerStatsUIController : MonoBehaviour, IHpListener, IStaminaListener, IFoodLevelListener
    {
        [SerializeField]
        private FillBarController HpBar;
        
        [SerializeField]
        private FillBarController StaminaBar;

        [SerializeField]
        private FillBarController FoodLevelBar;

        public void Init()
        {
            var playerStats = PlayerController.Instance.Stats;
            HpBar.Init(playerStats.Hp.Current, playerStats.Hp.Max);
            StaminaBar.Init(playerStats.Stamina.Current, playerStats.Stamina.Max);
            FoodLevelBar.Init(playerStats.FoodLevel.Current, playerStats.FoodLevel.Max);

            var messageManager = ManagerProvider.Instance.Get<IMessageManager>();
            messageManager.Subscribe<IHpMessage>(this);
            messageManager.Subscribe<IStaminaMessage>(this);
            messageManager.Subscribe<IFoodLevelMessage>(this);
        }

        public void OnMessageReceived(IStaminaMessage message)
        {
            StaminaBar.UpdateValue(message.Stamina.Current);
        }
        public void OnMessageReceived(IHpMessage message)
        {
            HpBar.UpdateValue(message.Hp.Current);
        }

        public void OnMessageReceived(IFoodLevelMessage message)
        {
            FoodLevelBar.UpdateValue(message.FoodLevel.Current);
        }
    }
}