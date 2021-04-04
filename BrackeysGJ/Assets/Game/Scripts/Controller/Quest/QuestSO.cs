using System.Collections.Generic;
using Game.Scripts.Controller.Itens;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/Quest", order = 2)]
    public class QuestSO : ScriptableObject
    {
        public string Name;
        public List<ItemSO> ItensRequired;
        public QuestSO SubQuest;
    }
}