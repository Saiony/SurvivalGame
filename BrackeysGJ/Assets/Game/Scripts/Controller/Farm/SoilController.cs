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

    public void Init()
    {
        SoilState = new List<SoilType>();
        SoilState.Add(SoilType.Normal);
    }

    public void Plow()
    {
        if (!SoilState.Contains(SoilType.Normal))
            throw new InvalidOperationException("Tried to plow a not Normal soil");
        SoilState.Remove(SoilType.Normal);
        SoilState.Add(SoilType.Plowed);

        SoilVfx.Plow();
    }

    public void Plant()
    {
        if (!SoilState.Contains(SoilType.Plowed))
            throw new InvalidOperationException("Tried to plant on a not Plowed soil");
        SoilState.Add(SoilType.Planted);

        SoilVfx.Plant();
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
        SoilState.Remove(SoilType.Planted);
        SoilState.Add(SoilType.Plowed);

        SoilVfx.Harvest();
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