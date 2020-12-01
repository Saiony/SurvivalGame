using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilController : MonoBehaviour
{
    [SerializeField]
    public List<SoilType> SoilState { get; private set; }

    [SerializeField]
    private SoilVFX _soilVfx = null;
    private SoilVFX SoilVfx => _soilVfx;

    [SerializeField]
    private CropController _crop = null;
    private CropController Crop => _crop;

    private int Exp { get; set; }

    public void Init()
    {
        SoilState = new List<SoilType>();
        SoilState.Add(SoilType.Normal);
        Exp = 0;
    }

    public void Plow()
    {
        if (!SoilState.Contains(SoilType.Normal))
            throw new InvalidOperationException("Tried to plow a not Normal soil");
        SoilState.Remove(SoilType.Normal);
        SoilState.Add(SoilType.Plowed);

        SoilVfx.Plow();
    }

    public void Plant(CropSO debugCrop)
    {
        if (!SoilState.Contains(SoilType.Plowed) || Crop.HasCrop)
            throw new InvalidOperationException("Tried to plant on a not Plowed soil");

        Crop.Init(debugCrop);

        SoilVfx.Plant(Crop.CurrentCropModel);
        SoilState.Add(SoilType.Planted);
    }

    public void Water()
    {
        if (!SoilState.Contains(SoilType.Plowed))
            throw new InvalidOperationException("Tried to water a not Plowed soil");
        SoilState.Add(SoilType.Watered);

        SoilVfx.Water();
    }

    public void Harvest()
    {
        if (!SoilState.Contains(SoilType.Plowed) || !SoilState.Contains(SoilType.Planted))
            throw new InvalidOperationException("Tried to harvest on an invalid soil");
        if (!Crop.Gatherable)
            throw new InvalidOperationException("Tried to harvest but crop isn't ready");

        Crop.OnHarvest();
        SoilState.Remove(SoilType.Planted);
        SoilVfx.Harvest();
    }

    public void OnDayPassed()
    {
        bool watered = SoilState.Contains(SoilType.Watered);
        if (Crop.HasCrop)
            Crop.OnDayPassed(watered);
        if (watered)
        {
            SoilState.Remove(SoilType.Watered);
            SoilVfx.UnWater();
        }
    }
}

public enum SoilType
{
    Unknown = 0,
    Normal = 1,
    Plowed = 2,
    Planted = 3,
    Watered = 4
}