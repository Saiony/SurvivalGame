using BrackeysGJ.Assets.Game.Scripts.Controller.Interface;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Message;
using BrackeysGJ.Assets.Game.Scripts.Manager;
using BrackeysGJ.Assets.Game.Scripts.Manager.Interface;
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
            HpBar.Init(100);
            StaminaBar.Init(50);

            var messageManager = ManagerProvider.Instance.Get<IMessageManager>();
            messageManager.Subscribe<IHpMessage>(this);
            messageManager.Subscribe<IStaminaMessage>(this);
        }
    }
}