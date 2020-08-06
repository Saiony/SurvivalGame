using System;
using System.Collections.Generic;
using Game.Scripts.Controller.NPC;
using UnityEngine;
using Game.Scripts.ScriptableObjects;
using System.Linq;

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
            if (!questSO)
                throw new Exception("Quest controller without a quest");

            Id = questSO.Id;
            ItensRequired = questSO.ItensRequired;
            Completed = false;
        }

        public bool ReceiveItem(Item.Item item)
        {
            var itemRequired = ItensRequired.FirstOrDefault(x => x.Equals(item));
            if (!itemRequired)
            {
                return false;
            }
            else
            {
                ItensRequired.Remove(itemRequired);
                if (ItensRequired.Count == 0)
                    FinishQuest();
                return true;
            }
        }

        private void FinishQuest()
        {
            Completed = true;
        }
    }
}