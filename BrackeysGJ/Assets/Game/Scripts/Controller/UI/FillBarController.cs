using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillBarController : MonoBehaviour
{
    [SerializeField]
    private Image FillImage;

    private float MaxValue;
    private float CurrentValue;

    public void Init(int maxValue)
    {
        MaxValue = maxValue;
    }

    public void UpdateValue(int newValue)
    {
        
    }
}

public interface IVerticalBar
{
    
}
