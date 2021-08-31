using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BrackeysGJ.Assets.Game.Scripts.Controller.UI
{
    public class ButtonController : Button
    {
        public ButtonClickedEvent onRightClick { get; private set; } = new ButtonClickedEvent();

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            if (eventData.button == PointerEventData.InputButton.Right)
                onRightClick.Invoke();
        }
    }
}
