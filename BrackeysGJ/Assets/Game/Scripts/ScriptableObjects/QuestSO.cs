using System.Collections.Generic;
using Game.Controller.Item;
using UnityEngine;

namespace Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/Quest", order = 2)]
    public class QuestSO : ScriptableObject
    {
        public List<Item> itensToCompleteQuest;
    }
}