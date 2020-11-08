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
        [SerializeField]
        private List<QuestSO> _mainQuests = null;
        private List<QuestSO> MainQuests => _mainQuests;

        private List<string> ActiveQuests { get; set; }

        private event Action OnQuestStarted = null;

        public static QuestsManager Instance = null;


        private void Awake()
        {
            if (Instance != null)
                throw new Exception("Singleton already populated.");
            Instance = this;

            if (MainQuests.Count == 0)
                throw new Exception("QuestsManager without quests");
            ActiveQuests = new List<string>();
            ActiveQuests.Add(MainQuests.FirstOrDefault().Name);
        }

        public void StartQuest(QuestController quest)
        {
            if (quest.HasSubQuest)
            {
                ActiveQuests.Add(quest.SubQuest);
            }
        }

        public void FinishQuest(QuestController quest)
        {
            if (!ActiveQuests.Exists(x => x == quest.Name))
                throw new Exception($"Tried to finish a quest that isn't on the current ones \nQuest: {quest.Name}");
            else //terminar a quest
            {
                var questToBeFinished = ActiveQuests.Where(x => x == quest.Name).FirstOrDefault();
                if (MainQuests.Any(x => x.Name == quest.Name))
                {
                    GoToNextQuest(questToBeFinished);
                    return;
                }
                ActiveQuests.Remove(questToBeFinished);
            }
        }

        private void GoToNextQuest(string quest)
        {
            var questIndex = MainQuests.IndexOf(MainQuests.FirstOrDefault(x => x.Name == quest));
            var nextQuest = MainQuests[questIndex + 1].Name;
            if (nextQuest == null)
            {
                GameOver();
                return;
            }
            ActiveQuests.Remove(quest);
            ActiveQuests.Add(nextQuest);
        }

        private void GameOver()
        {
            Debug.Log("Jogo acabou, parabéns por ser um otário");
        }

        public bool IsActiveQuest(string questName)
        {
            return ActiveQuests.Any(x => x == questName);
        }

        public void SubscribeOnQuestFinished(Action action)
        {
            OnQuestStarted += action;
        }

        public void UnsubscribeOnQuestFinished(Action action)
        {
            OnQuestStarted -= action;
        }
    }
}