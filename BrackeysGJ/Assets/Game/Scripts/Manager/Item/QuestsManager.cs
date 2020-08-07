using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.Controller.Quest;
using System;
using System.Linq;
using Game.Scripts.ScriptableObjects;

namespace Game.Scripts.Manager.Quest
{

    public class QuestsManager : MonoBehaviour
    {
        public List<QuestSO> Quests = null;
        public static QuestsManager Instance = null;
        public QuestSO CurrentQuest;

        void Awake()
        {
            if (Instance != null)
                throw new Exception("Singleton already populated.");
            Instance = this;
        }

        private void Start()
        {
            if(Quests.Count == 0)
                throw new Exception("QuestsManager without quests");
            CurrentQuest = Quests.First();
        }

        public void FinishQuest(QuestController quest)
        {
            Quests.FirstOrDefault(x => x.Name == quest.Name);
        }
    }
}