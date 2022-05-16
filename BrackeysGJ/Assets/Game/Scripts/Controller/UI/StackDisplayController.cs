using DG.Tweening;
using Game.Scripts.Domain.Interface.Items;
using Game.Scripts.Domain.Items;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Controller.UI
{
    public class StackDisplayController : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _quantityCanvas = null;
        private CanvasGroup QuantityCanvas => _quantityCanvas;

        [SerializeField]
        private TextMeshProUGUI _quantityText = null;
        private TextMeshProUGUI QuantityText => _quantityText;

        public void Init()
        {
            QuantityCanvas.DOFade(0, 0).Play();
        }

        public Tween Clear(bool withFade = false)
        {
            var duration = withFade ? 0.15f : 0f;
            Sequence seq = DOTween.Sequence();
            seq.Append(QuantityCanvas.DOFade(0, duration));
            return seq;
        }

        public void DisplayQuantity(IItem item)
        {
            if (item is Tool || item is Weapon)
            {
                Clear(true).Play();
                return;
            }

            QuantityText.text = item.Quantity.ToString();
            QuantityCanvas.DOFade(item.Quantity > 0 ? 1 : 0, 0.15f).Play();
        }
    }
}
