using System;
using Game.Scripts.Controller.Item;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class TimeChangeController : MonoBehaviour
{
    public Button rewindButton;
    public Button fowardButton;


    private static TimeChangeController singleton;
    private bool IsShowingScreen { get; set; }
    private Item Item { get; set; }
    private Canvas Canvas { get; set; }

    void Awake()
    {
        if (singleton != null)
            throw new ArgumentOutOfRangeException(nameof(singleton));
        singleton = this;
        Canvas = this.GetComponent<Canvas>();
        Canvas.enabled = false;
    }

    public static void Instantiate(Item item)
    {
        if (singleton.IsShowingScreen)
            return;
        if (!item.Rewindable && !item.Fowardable)
        {
            print("item not usable.");
            return;
        }
        singleton.IsShowingScreen = true;
        singleton.Item = item;
        singleton.Canvas.enabled = true;
        HideAllButtons();
    }

    private static void HideAllButtons()
    {
        singleton.rewindButton.gameObject.SetActive(false);
        singleton.fowardButton.gameObject.SetActive(false);
    }

    private static void ShowOnlyAvailableButtons(Item item)
    {
        if (item.Rewindable)
            singleton.rewindButton.gameObject.SetActive(true);
        if (item.Fowardable)
            singleton.fowardButton.gameObject.SetActive(true);
    }

    public void Foward()
    {
        Item.FowardTime();
    }

    public void Rewind()
    {
        Item.RewindTime();
    }
}
