using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Helper;
using UnityEngine;

public class Crop
{
    public List<int> ExpToLvlUp { get; private set; }
    public List<GameObject> CropModels { get; private set; }
    public SeasonType Season { get; private set; }
    public int LvlToGather { get; private set; }
    public int DaysToRot { get; private set; }
    public Item Item { get; private set; }

    public Crop()
    {
        ExpToLvlUp = null;
        CropModels = null;
        Season = SeasonType.Unknown;
        LvlToGather = 0;
        DaysToRot = 0;
        Item = null;
    }

    public Crop(List<int> expToLvlUp, List<GameObject> cropModels, SeasonType season, int lvlToGather, int daysToRot, Item item) : this()
    {
        SetExpToLvlUp(expToLvlUp);
        SetCropModels(cropModels);
        SetSeason(season);
        SetLvlToGather(lvlToGather);
        SetDaysToRot(daysToRot);
        SetItem(item);
    }

    private void SetExpToLvlUp(List<int> expToLvlUp)
    {
        if (expToLvlUp == null || expToLvlUp.Count == 0)
            throw new InvalidOperationException("ExpToLvlUp null or empty");

        ExpToLvlUp = expToLvlUp.ToList();
    }

    private void SetCropModels(List<GameObject> cropModels)
    {
        if (cropModels == null || cropModels.Count == 0)
            throw new InvalidOperationException("CropModels is null or empty");

        CropModels = cropModels.ToList();
    }

    private void SetSeason(SeasonType season)
    {
        if (season == SeasonType.Unknown)
            throw new InvalidOperationException("Season is Unknown");

        Season = season;
    }

    private void SetLvlToGather(int lvlToGather)
    {
        if (lvlToGather < 0)
            throw new InvalidOperationException("LvlToGather is negative");

        LvlToGather = lvlToGather;
    }

    private void SetDaysToRot(int daysToRot)
    {
        if (daysToRot < 0)
            throw new InvalidOperationException("DaysToRot is negative");

        DaysToRot = daysToRot;
    }

    private void SetItem(Item item)
    {
        if (item == null)
            throw new InvalidOperationException("Item is null");

        Item = item;
    }
}
