using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillBarController : MonoBehaviour
{
    [SerializeField]
    private Image FillImage;

    private float MaxValue { get; set; }
    private float CurrentValue { get; set; }

    public void Init(int currentValue, int maxValue)
    {
        MaxValue = maxValue;
        UpdateValue(currentValue);
    }

    public void UpdateValue(int newValue)
    {
        CurrentValue = newValue;
        FillImage.fillAmount = CurrentValue / MaxValue;
    }
}

public interface IVerticalBar
{
    
}
