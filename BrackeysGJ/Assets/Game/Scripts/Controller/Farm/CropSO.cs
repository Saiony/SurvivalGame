using System.Collections.Generic;
using Game.Helper;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Crop", menuName = "ScriptableObjects/Crop", order = 1)]
    public class CropSO : ScriptableObject
    {
        public ItemSO Item;
        public List<int> ExpToLvlUp;
        public int LvlToGather;
        public int DaysToRot;
        public SeasonType Season;
        public List<GameObject> CropModels;
    }
}