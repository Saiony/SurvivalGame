using System.Collections.Generic;
using Game.Controller.Item;
using UnityEngine;

namespace Game.ScriptableObject
{
    [CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/Quest", order = 2)]
    public class Quest : UnityEngine.ScriptableObject
    {
        public List<Item> itensToCompleteQuest;
    }
}