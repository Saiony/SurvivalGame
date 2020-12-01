using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crop", menuName = "ScriptableObjects/Crop", order = 1)]
public class CropSO : ScriptableObject
{
    public List<int> ExpToLvlUp;
    public List<GameObject> CropModels;
}
