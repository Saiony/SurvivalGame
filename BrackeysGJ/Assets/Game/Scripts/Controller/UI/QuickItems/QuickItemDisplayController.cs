using System.Collections;
using System.Collections.Generic;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Items;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuickItemDisplayController : MonoBehaviour
{
    [SerializeField]
    private Image _displayImage = null;
    private Image DisplayImage => _displayImage;

    [SerializeField]
    private Transform _itemSelector = null;
    private Transform ItemSelector => _itemSelector;

    [SerializeField]
    private StackDisplayController _stack = null;
    private StackDisplayController stack => _stack;

    public IItem Item { get; private set; }

    private void Awake()
    {
        ItemSelector.gameObject.SetActive(false);
        ItemSelector.localScale = Vector3.zero;
    }

    public void SetItem(IItem item)
    {
        Item = item;
        if (Item == null)
        {
            Clear();
            return;
        }

        DisplayImage.sprite = Item.Image;
        DisplayImage.DOFade(1, 0).Play();
        stack.DisplayQuantity(item);
    }

    private void Clear()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(DisplayImage.DOFade(0, 0));
        seq.Append(stack.Clear());
        seq.Play();
    }

    public void Select()
    {
        Sequence seq = DOTween.Sequence();
        seq.Insert(0, ItemSelector.DOScale(Vector3.one, 0.15f));
        seq.InsertCallback(0.07f, () => ItemSelector.gameObject.SetActive(true));
        seq.Play();
    }

    public void Deselect()
    {
        Sequence seq = DOTween.Sequence();
        seq.Insert(0, ItemSelector.DOScale(Vector3.one * 0.7f, 0.15f));
        seq.InsertCallback(0.07f, () => ItemSelector.gameObject.SetActive(false));
        seq.Play();
    }
}
