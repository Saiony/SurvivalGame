using System;
using System.Collections.Generic;
using Game.Scripts.Controller.NPC;
using UnityEngine;
using Game.Scripts.ScriptableObjects;
using System.Linq;
using Game.Scripts.Manager.Quest;

namespace Game.Scripts.Controller.Quest
{
    public class QuestController : MonoBehaviour
    {
        [SerializeField]
        private QuestSO questSO = null;

        private List<string> ItensRequired;
        
        [NonSerialized]
        public string Name;
        [NonSerialized]
        public bool Started;
        [NonSerialized]
        public bool Completed;

        private void Start()
        {
            if (!questSO)
                throw new Exception("Quest controller without a quest");

            Name = questSO.Name;
            ItensRequired = new List<string>();
            foreach(var itenRequiredSO in questSO.ItensRequired)
                ItensRequired.Add(itenRequiredSO.name);
            Completed = false;
        }

        public bool ReceiveItem(Item.Item item)
        {
            var itemReceived = ItensRequired.FirstOrDefault(x => x == item.name);
            if (itemReceived == null)
            {
                return false;
            }
            else
            {
                ItensRequired.Remove(itemReceived);
                if (ItensRequired.Count == 0)
                    FinishQuest();
                return true;
            }
        }

        public void StartQuest()
        {
            Started = true;
        }        

        public void FinishQuest()
        {
            QuestsManager.Instance.FinishQuest(this);
            Started = false;
            Completed = true;
        }
    }
}