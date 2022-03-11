using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Items;
using BrackeysGJ.Assets.Game.Scripts.Controller.UI;
using Game.Scripts.Controller.Player;
using BrackeysGJ.Assets.Game.Scripts.Domain.Items;

public abstract class BaseItemDisplayController : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    private Image _displayImage = null;
    protected Image DisplayImage => _displayImage;

    [SerializeField]
    private ButtonController _selectButton = null;
    protected ButtonController SelectButton => _selectButton;

    [SerializeField]
    private Color _selectedColor = Color.white;
    private Color SelectedColor => _selectedColor;

    [SerializeField]
    private StackDisplayController _stack = null;
    private StackDisplayController stack => _stack;

    public IItem ItemDisplayed { get; private set; }
    protected IPlayerItems PlayerItems { get; set; }
    private InventoryItemDisplayListener Listener { get; set; }

    protected void BaseInit(InventoryItemDisplayListener listener)
    {
        Listener = listener;
        PlayerItems = PlayerController.Instance.Items;
        SelectButton.onClick.AddListener(OnItemLeftClick);
        SelectButton.onRightClick.AddListener(OnItemRightClick);

        DisplayItem(null);
    }

    public void DisplayItem(IItem item)
    {
        if (item == null)
        {
            Clear();
            return;
        }
        DisplayImage.sprite = item.Image;
        DisplayImage.DOFade(1, 0).Play();

        stack.DisplayQuantity(item);
        ItemDisplayed = item;
    }

    public void SetItem(IItem item)
    {
        ItemDisplayed = item;
        OnItemAdded(item);
    }

    public void Consume()
    {
        OnItemConsumed(ItemDisplayed);
    }

    public void Clear()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(DisplayImage.DOFade(0, 0));
        seq.Append(stack.Clear());
        seq.Play();
        OnItemCleared();
    }

    public void Select()
    {
        DisplayImage.color = SelectedColor;
    }

    public void Deselect()
    {
        DisplayImage.color = Color.white;
    }

    private void OnItemLeftClick()
    {
        Listener?.OnItemDisplayClickedLeft(this);
    }

    private void OnItemRightClick()
    {
        Listener?.OnItemDisplayClickedRight(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Listener?.OnItemDisplayHovered(this);
    }

    public abstract void OnItemAdded(IItem item);

    public virtual void OnItemConsumed(IItem item)
    {
    }

    protected virtual void OnItemCleared()
    {
    }
}

public interface InventoryItemDisplayListener
{
    void OnItemDisplayClickedLeft(BaseItemDisplayController itemDisplay);
    void OnItemDisplayClickedRight(BaseItemDisplayController itemDisplay);
    void OnItemDisplayHovered(BaseItemDisplayController itemDisplay);
}