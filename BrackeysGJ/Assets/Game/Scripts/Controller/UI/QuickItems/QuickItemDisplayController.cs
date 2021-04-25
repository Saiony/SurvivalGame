using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class QuickItemDisplayController : MonoBehaviour
{
    [SerializeField]
    private Image _displayImage = null;
    private Image DisplayImage => _displayImage;

    public Item Item { get; private set; }

    public void SetItem(Item item)
    {
        Item = item;
        if (Item == null)
        {
            Clear();
            return;
        }

        DisplayImage.sprite = Item.Image;
        DisplayImage.DOFade(1, 0).Play();
    }

    private void Clear()
    {
        DisplayImage.DOFade(0, 0).Play();
    }
}
