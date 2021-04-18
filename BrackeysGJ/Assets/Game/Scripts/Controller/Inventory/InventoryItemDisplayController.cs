using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InventoryItemDisplayController : MonoBehaviour
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
    }

    public void Clear()
    {
        DisplayImage.DOFade(0, 0).Play();
    }

    private void OnItemClick()
    {
        Listener?.OnItemDisplayClicked(this);
    }

    public void Select()
    {
        DisplayImage.color = SelectedColor;
    }

    public void Deselect()
    {
        DisplayImage.color = Color.white;
    }
}

public interface InventoryItemDisplayListener
{
    void OnItemDisplayClicked(InventoryItemDisplayController itemDisplay);
}