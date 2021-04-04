using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class InventoryDisplayController : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _modal = null;
    private CanvasGroup Modal => _modal;

    [SerializeField]
    private InventoryController _inventory = null;
    private InventoryController Inventory => _inventory;

    [SerializeField]
    private List<InventoryItemDisplayController> _quickItems = null;
    private List<InventoryItemDisplayController> QuickItems => _quickItems;

    [SerializeField]
    private List<InventoryItemDisplayController> _displayItems = null;
    private List<InventoryItemDisplayController> DisplayItems => _displayItems;

    private bool Showing { get; set; }
    public static InventoryDisplayController Instance = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        Modal.gameObject.SetActive(false);

    }

    public void Toggle()
    {
        if (Showing)
            Hide();
        else
            Show();
    }

    private void Hide()
    {
        Showing = false;
        Sequence seq = DOTween.Sequence();

        seq.Append(Modal.DOFade(0, 0.3f));
        seq.AppendCallback(() => Modal.gameObject.SetActive(false));
        seq.Play();
    }

    private void Show()
    {
        Showing = true;
        Clear();
        Modal.gameObject.SetActive(true);
        Sequence seq = DOTween.Sequence();

        seq.AppendCallback(() => Modal.DOFade(1, 0.3f));
        seq.Play();

        var items = Inventory.Items;
        for (int i = 0; i < items.Count; i++)
        {
            DisplayItems[i].Init(items[i]);
        }
    }

    private void Clear()
    {
        QuickItems.ForEach(item => item.Clear());
        DisplayItems.ForEach(item => item.Clear());
    }
}
