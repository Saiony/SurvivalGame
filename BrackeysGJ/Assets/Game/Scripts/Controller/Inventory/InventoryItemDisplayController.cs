using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemDisplayController : MonoBehaviour
{
    [SerializeField]
    private Image _displayImage = null;
    private Image DisplayImage => _displayImage;

    public Item Item { get; private set; }

    public void Init(Item item)
    {
        Item = item;
        DisplayImage.sprite = Item.Image;
        DisplayImage.enabled = true;
    }

    public void Clear()
    {
        DisplayImage.enabled = false;
    }
}
