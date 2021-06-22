using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

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

    public Tween Clear()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(QuantityCanvas.DOFade(0, 0));
        return seq;
    }

    public void DisplayQuantity(Item item)
    {
        if (item is Tool)
        {
            Clear().Play();
            return;
        }

        QuantityText.text = item.Quantity.ToString();
        QuantityCanvas.DOFade(item.Quantity > 0 ? 1 : 0, 0.15f).Play();
    }
}
