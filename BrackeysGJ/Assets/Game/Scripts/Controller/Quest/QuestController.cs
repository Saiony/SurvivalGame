using System;
using System.Collections.Generic;
using Game.Scripts.Controller.NPC;
using UnityEngine;
using Game.Scripts.ScriptableObjects;

namespace Game.Scripts.Controller.Quest
{
    public class QuestController : MonoBehaviour
    {        
        public QuestSO questSO = null;
        public int Id;
        public List<Item.Item> ItensRequired;
        
        public bool Started;
        public bool Completed;

        private void Start() 
        {
            if(!questSO)    
                throw new Exception("Quest controller withou a quest");
            
            Id = questSO.Id;
            ItensRequired = questSO.ItensRequired;
            Completed = false;
        }

        public void FinishQuest()
        {
            Completed = true;
        }
    }
}