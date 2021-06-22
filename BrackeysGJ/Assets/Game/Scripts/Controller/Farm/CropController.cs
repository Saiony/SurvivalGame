using System;
using System.Collections.Generic;
using Game.Helper;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

public class CropController : MonoBehaviour
{
    private int Lvl { get; set; }
    private int Exp { get; set; }
    public GameObject CurrentCropModel { get; private set; }
    private int ConsecutiveDaysWithoutWater;

    [SerializeField]
    private Transform _cropSpot = null;
    private Transform CropSpot => _cropSpot;

    public Crop Crop { get; set; }

    //TODO: jogar essas regras para Crop
    private bool Rotten { get; set; }
    public bool HasCrop => CurrentCropModel != null;
    public bool Gatherable => (Lvl >= Crop.LvlToGather) && !Rotten;

    public void Init(CropSO cropSO)
    {
        Lvl = 0;
        Exp = 0;
        ConsecutiveDaysWithoutWater = 0;
        Rotten = false;

        var cropItem = new Misc(cropSO.Item.Id, cropSO.Item.name, cropSO.Item.Description, cropSO.Item.Image);
        Crop = new Crop(cropSO.ExpToLvlUp, cropSO.CropModels, cropSO.Season, cropSO.LvlToGather, cropSO.DaysToRot, cropItem);

        CurrentCropModel = Instantiate(Crop.CropModels[0], CropSpot.position, Quaternion.identity, transform);
    }

    public void OnHarvest()
    {
        Crop = null;
        Destroy(CurrentCropModel);
        CurrentCropModel = null;
    }

    public void OnDayPassed(bool soilWatered)
    {
        if (soilWatered)
        {
            ConsecutiveDaysWithoutWater = 0;
            if (Rotten)
                return;

            Exp++;
            if (Exp >= Crop.ExpToLvlUp[Lvl])
                LvlUp();
        }
        else
        {
            ConsecutiveDaysWithoutWater++;
            if (ConsecutiveDaysWithoutWater >= Crop.DaysToRot)
                Rot();
        }
    }

    public void OnSeasonChanged(SeasonType currentSeason)
    {
        if (HasCrop && Crop.Season != currentSeason)
        {
            Debug.Log("[SoilController] Crop season different than current. Make it ROT");
            Rot();
        }
    }

    private void LvlUp()
    {
        if (Lvl >= Crop.LvlToGather)
            Debug.Log("Crop at maximum level");
        Lvl++;
        Exp = 0;
        DestroyImmediate(CurrentCropModel, true);
        CurrentCropModel = Instantiate(Crop.CropModels[Lvl], transform.position, Quaternion.identity, transform);
    }

    private void Rot()
    {
        Rotten = true;
        DestroyImmediate(CurrentCropModel, true);
        CurrentCropModel = Instantiate(Crop.CropModels[Crop.LvlToGather + 1], transform.position, Quaternion.identity, transform);
    }
}

public enum CropState
{
    Unknown = 0,
    Seed = 1,
    Sprout = 2,
    Intermediate = 3,
    Ripe = 4,
    Rotten = 5
}