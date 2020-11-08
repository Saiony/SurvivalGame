using System;
using System.Collections.Generic;
using Game.Scripts.Controller.NPC;
using UnityEngine;
using Game.Scripts.ScriptableObjects;
using System.Linq;
using Game.Scripts.Manager.Quest;
using Game.Scripts.Controller.Item;

namespace Game.Scripts.Controller.Quest
{
    public class QuestController : MonoBehaviour
    {
        [SerializeField]
        private QuestSO _questSO = null;
        private QuestSO questSO => _questSO;

        private List<string> ItensRequired { get; set; }

        //[NonSerialized]
        public string Name { get; private set; }

        //[NonSerialized]
        public string SubQuest { get; private set; }

        public bool Started { get; private set; }
        public bool Completed { get; private set; }

        public bool HasSubQuest => SubQuest != null;

        private void Start()
        {
            if (!questSO)
                throw new Exception("Quest controller without a quest");

            //Setup
            Name = questSO.Name;

            SubQuest = questSO.SubQuest != null ? questSO.SubQuest.Name : null;

            ItensRequired = new List<string>();
            foreach (var itenRequiredSO in questSO.ItensRequired)
                ItensRequired.Add(itenRequiredSO.name);
            Completed = false;

            QuestsManager.Instance.SubscribeOnQuestFinished(OnAnyQuestFinished);
        }

        public bool ReceiveItem(ItemController item)
        {
            var itemReceived = ItensRequired.FirstOrDefault(x => x == item.Id);

            if (itemReceived == null)
                return false;
            else
            {
                Debug.Log(item.name + " Received");
                ItensRequired.Remove(itemReceived);
                if (ItensRequired.Count == 0)
                    FinishQuest();
                return true;
            }
        }

        public void StartQuest()
        {
            Started = true;
            QuestsManager.Instance.StartQuest(this);
        }

        public void FinishQuest()
        {
            QuestsManager.Instance.FinishQuest(this);
            Started = false;
            Completed = true;
            QuestsManager.Instance.UnsubscribeOnQuestFinished(OnAnyQuestFinished);
        }

        public void OnAnyQuestFinished()
        {
            //ver se eu sou a current
        }
    }
}