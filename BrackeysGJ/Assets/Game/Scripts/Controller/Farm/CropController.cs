using System;
using System.Collections.Generic;
using UnityEngine;

public class CropController : MonoBehaviour
{
    private int Lvl { get; set; }
    private int Exp { get; set; }
    private List<int> ExpToLvlUp { get; set; }
    private List<GameObject> CropModels { get; set; }
    public GameObject CurrentCropModel { get; private set; }
    private int ConsecutiveDaysWithoutWater;

    [SerializeField]
    private Transform _cropSpot = null;
    private Transform CropSpot => _cropSpot;

    private bool Rotten { get; set; }
    public bool HasCrop => CurrentCropModel != null;
    public bool Gatherable => (Lvl >= LvlToGather) && !Rotten;


    //TODO: colocar no SO
    private int LvlToGather => 3;
    private int DaysToRot => 2;


    public void Init(CropSO cropSO)
    {
        Lvl = 0;
        Exp = 0;
        ConsecutiveDaysWithoutWater = 0;
        Rotten = false;

        ExpToLvlUp = new List<int>();
        cropSO.ExpToLvlUp.ForEach(x => ExpToLvlUp.Add(x));

        CropModels = new List<GameObject>();
        cropSO.CropModels.ForEach(x => CropModels.Add(x));

        CurrentCropModel = Instantiate(CropModels[0], CropSpot.position, Quaternion.identity, transform);
    }

    public void OnHarvest()
    {
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
            if (Exp >= ExpToLvlUp[Lvl])
                LvlUp();
        }
        else
        {
            ConsecutiveDaysWithoutWater++;
            if (ConsecutiveDaysWithoutWater >= DaysToRot)
                Rot();
        }
    }

    private void LvlUp()
    {
        if (Lvl >= LvlToGather)
            throw new InvalidOperationException("Crop at maximum level");
        Lvl++;
        Exp = 0;
        DestroyImmediate(CurrentCropModel, true);
        CurrentCropModel = Instantiate(CropModels[Lvl], transform.position, Quaternion.identity, transform);
    }

    private void Rot()
    {
        Rotten = true;
        DestroyImmediate(CurrentCropModel, true);
        CurrentCropModel = Instantiate(CropModels[LvlToGather + 1], transform.position, Quaternion.identity, transform);
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