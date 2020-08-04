using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.Controller.Quest;
using System;
using System.Linq;

namespace Game.Scripts.Manager.Quest
{

    public class QuestsManager : MonoBehaviour
    {
        public List<QuestController> Quests = null;
        public static QuestsManager Instance = null;
        public int QuestCounter = 0;
        public QuestController CurrentQuest => Quests[QuestCounter];
        
        void Awake()
        {
            if (Instance != null)
                throw new Exception("Singleton already populated.");
            Instance = this;
        }

        private void Start()
        {
            
        }

        public void FinishQuest(QuestController quest)
        {
            Quests.Select(x => x.questSO.id == quest.questSO.id);
        }
    }
}