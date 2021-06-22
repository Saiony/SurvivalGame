using System;
using System.Collections;
using System.Collections.Generic;
using Game.Helper;
using Game.Scripts.ScriptableObjects;
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
            return;

        SoilState.Remove(SoilType.Normal);
        SoilState.Add(SoilType.Plowed);

        SoilVfx.Plow();
    }

    public void Plant(CropSO debugCrop)
    {
        if (!SoilState.Contains(SoilType.Plowed) || Crop.HasCrop)
            return;

        Crop.Init(debugCrop);

        SoilVfx.Plant(Crop.CurrentCropModel);
        SoilState.Add(SoilType.Planted);
    }

    public void Water()
    {
        if (!SoilState.Contains(SoilType.Plowed))
            return;
        SoilState.Add(SoilType.Watered);

        SoilVfx.Water();
    }

    public Crop Harvest()
    {
        if (!SoilState.Contains(SoilType.Plowed) || !SoilState.Contains(SoilType.Planted))
            return null;
        if (!Crop.Gatherable)
            return null;

        var crop = Crop.Crop;
        Crop.OnHarvest();
        SoilState.Remove(SoilType.Planted);
        SoilVfx.Harvest();

        return crop;
    }

    public void OnDayChanged()
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

    public void OnSeasonChanged(SeasonType currentSeason)
    {
        if (Crop == null)
            return;
        Crop.OnSeasonChanged(currentSeason);
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