using System.Collections.Generic;
using Game.Scripts.Controller.Item;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/Quest", order = 2)]
    public class QuestSO : ScriptableObject
    {
        public int Id;
        public List<Item> ItensRequired;
    }
}