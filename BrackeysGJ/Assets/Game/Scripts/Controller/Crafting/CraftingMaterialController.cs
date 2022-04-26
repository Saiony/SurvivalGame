using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Scripts.Domain.Crafting;

public class CraftingMaterialController : MonoBehaviour
{
    [SerializeField]
    private Image Image;

    [SerializeField]
    private TextMeshProUGUI Quantity;

    [SerializeField]
    private Image MaterialBG;

    [SerializeField]
    private Image QuantityBG;

    [SerializeField]
    private Color InsufficientColor;

    private Color defaultBGColor;

    public void Init()
    {
        defaultBGColor = MaterialBG.color;
    }

    public void Show(CraftingMaterial material, int playerQuantity)
    {
        Image.sprite = material.Item.Image;
        Quantity.text =  playerQuantity.ToString() + "/" +material.Quantity.ToString();

        var bgColor = playerQuantity < material.Quantity ? InsufficientColor : defaultBGColor;
        MaterialBG.color = bgColor;
        QuantityBG.color = bgColor;

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
