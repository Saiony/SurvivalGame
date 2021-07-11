using System.Collections;
using System.Collections.Generic;
using Game.Helper;
using Game.Scripts.Controller.Player;
using Game.Scripts.Controller.Time;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

public class PlantationManager : MonoBehaviour, IPlowable, IWaterable, IPlantable, IPickable
{
    [SerializeField]
    private Vector2 _plantationGrid = Vector2.zero;
    private Vector2 PlantationGrid => _plantationGrid;

    [SerializeField]
    private Vector2 _soilSize = Vector2.zero;
    private Vector2 SoilSize => _soilSize;

    [SerializeField]
    private GameObject _soilPrefab = null;
    private GameObject SoilPrefab => _soilPrefab;

    [SerializeField]
    private Collider _detectionCollider = null;
    public Collider DetectionCollider => _detectionCollider;

    [Header("Debug")]
    [SerializeField]
    private CropSO _debugCrop = null;
    private CropSO DebugCrop => _debugCrop;

    private List<List<SoilController>> SoilList { get; set; }

    private void Start()
    {
        var area = CalculatePlantationArea();

        CreateSoils();
        PositionSoils(area);
        ((BoxCollider)DetectionCollider).size = new Vector3(area.x * SoilSize.x, 1, area.y * SoilSize.y);
        ((BoxCollider)DetectionCollider).center = new Vector3((area.x - 1) * SoilSize.x / 2, 0.5f, (area.y - 1) * SoilSize.y / 2);
        TimeController.Instance.SubscribeDayChanged(OnDayChanged);
        TimeController.Instance.SubscribeSeasonChanged(OnSeasonChanged);
    }

    private Vector2 CalculatePlantationArea()
    {
        var x = SoilSize.x * PlantationGrid.x;
        var y = SoilSize.y * PlantationGrid.y;
        return new Vector2(x, y);
    }

    private void CreateSoils()
    {
        SoilList = new List<List<SoilController>>();
        for (int i = 0; i < PlantationGrid.x; i++)
        {
            SoilList.Add(new List<SoilController>());

            for (int j = 0; j < PlantationGrid.y; j++)
            {
                var soilGO = Instantiate(SoilPrefab, transform);
                SoilList[i].Add(soilGO.GetComponent<SoilController>());
                SoilList[i][j].Init();
            }
        }

        //DEBUG SOILS
        SoilList[0][0].Plow();
        SoilList[0][0].Plant(DebugCrop);
        for (int i = 0; i < 7; i++)
        {
            SoilList[0][0].Water();
            SoilList[0][0].OnDayChanged();
        }
    }

    private void PositionSoils(Vector2 plantationArea)
    {
        for (int x = 0; x < SoilList.Count; x++)
        {
            for (int z = 0; z < SoilList[x].Count; z++)
            {
                var soilTransform = SoilList[x][z].GetComponent<Transform>();
                var posX = x * SoilSize.x;
                var posZ = z * SoilSize.y;
                soilTransform.localPosition = new Vector3(posX, transform.position.y, posZ);
            }
        }
    }

    private SoilController GetSoilController(Vector3 inputPos)
    {
        var relativePos = inputPos - transform.position;

        var soilIndexX = Mathf.RoundToInt((relativePos.x / SoilSize.x));
        var soilIndexY = Mathf.RoundToInt((relativePos.z / SoilSize.y));

        return SoilList[soilIndexX][soilIndexY];
    }

    public void OnPick(Vector3 pos)
    {
        var soil = GetSoilController(pos);
        var crop = soil.Harvest();
        PlayerController.Instance.GiveItem(crop.Item);
        Debug.Log("Command recebido -> Interact", soil.gameObject);
    }

    public void OnPlow(Vector3 pos)
    {
        var soil = GetSoilController(pos);
        soil.Plow();
        Debug.Log("Command recebido -> Plow", soil.gameObject);
    }

    public void OnPlant(Vector3 pos)
    {
        var soil = GetSoilController(pos);
        soil.Plant(DebugCrop);
        Debug.Log("Command recebido -> Plant", soil.gameObject);
    }

    public void OnWater(Vector3 pos)
    {
        var soil = GetSoilController(pos);
        soil.Water();
        Debug.Log("Command recebido -> Water", soil.gameObject);
    }

    public void OnDayChanged()
    {
        Debug.Log("PlantationManager -> Day Changed");
        SoilList.ForEach(x => x.ForEach(y => y.OnDayChanged()));
    }

    public void OnSeasonChanged()
    {
        Debug.Log("PllantationManager -> Season Changed");
        var currentSeason = TimeController.Instance.GetSeason();
        SoilList.ForEach(x => x.ForEach(y => y.OnSeasonChanged(currentSeason)));
    }
}
