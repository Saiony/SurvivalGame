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
            if(CurrentQuest.Name != quest.Name)
                throw new Exception($"Tried to finish a quest that isn't the current one \nCurrentQuest: {CurrentQuest.Name} \nQuest: {quest.Name}");
            if(Quests.IndexOf(CurrentQuest) >= Quests.Count-1) //Última quest do jogo
                Debug.Log("Jogo acabou, parabéns por ser um otário");
            else
                CurrentQuest = Quests[Quests.IndexOf(CurrentQuest) + 1];    
        }
    }
}