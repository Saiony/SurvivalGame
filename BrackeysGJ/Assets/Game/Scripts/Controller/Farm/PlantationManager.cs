using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Controller.Interact;
using Game.Scripts.Controller.Player;
using UnityEngine;

public class PlantationManager : Interactable
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

    private List<List<SoilController>> SoilList { get; set; }

    protected override void OnDidStart()
    {
        var area = CalculatePlantationArea();

        CreateSoils();
        PositionSoils(area);
        ((BoxCollider)DetectionCollider).size = new Vector3(area.x * SoilSize.x, 1, area.y * SoilSize.y);
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

    protected override void OnPlayerEnter()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnPlayerExit()
    {
        throw new System.NotImplementedException();
    }
    private int interactStateMachineFakeGps = 0;

    //TODO: Bolar uma forma de saber a posição do interact e qual ferramenta usada
    protected override void OnPlayerInteract()
    {
        Debug.Log("Player Interacted with PlantationManager");
        var playerPos = PlayerController.Instance.transform.position;
        var soil = GetSoilController(playerPos);
        Debug.Log("Interacted with Soil: ", soil.gameObject);

        switch (interactStateMachineFakeGps)
        {
            case 0:
                soil.Plow();
                break;
            case 1:
                soil.Plant();
                break;
            case 2:
                soil.Water();
                break;
            default:
                break;
        }
        //interactStateMachineFakeGps++;
    }
}
