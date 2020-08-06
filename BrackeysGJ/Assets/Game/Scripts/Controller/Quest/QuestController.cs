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
        private List<InteractableItemSO> ItensRequired;
        
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

        public void FinishQuest()
        {
            QuestsManager.Instance.FinishQuest(this);
            Completed = true;
        }
    }
}