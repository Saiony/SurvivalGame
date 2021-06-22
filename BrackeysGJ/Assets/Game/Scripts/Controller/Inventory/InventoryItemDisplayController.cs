using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryItemDisplayController : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    private Image _displayImage = null;
    private Image DisplayImage => _displayImage;

    [SerializeField]
    private Button _selectButton = null;
    private Button SelectButton => _selectButton;

    [SerializeField]
    private Color _selectedColor = Color.white;
    private Color SelectedColor => _selectedColor;

    [SerializeField]
    private StackDisplayController _stack = null;
    private StackDisplayController stack => _stack;

    public Item Item { get; private set; }
    private InventoryItemDisplayListener Listener { get; set; }

    public void Init(InventoryItemDisplayListener listener)
    {
        Listener = listener;
        SelectButton.onClick.AddListener(OnItemClick);
    }

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

        stack.DisplayQuantity(item);
    }

    public void Clear()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(DisplayImage.DOFade(0, 0));
        seq.Append(stack.Clear());
        seq.Play();
    }

    public void Select()
    {
        DisplayImage.color = SelectedColor;
    }

    public void Deselect()
    {
        DisplayImage.color = Color.white;
    }

    private void OnItemClick()
    {
        Listener?.OnItemDisplayClicked(this);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Listener?.OnItemDisplayHovered(this);
    }
}

public interface InventoryItemDisplayListener
{
    void OnItemDisplayClicked(InventoryItemDisplayController itemDisplay);
    void OnItemDisplayHovered(InventoryItemDisplayController itemDisplay);
}