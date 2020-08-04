using Game.ScriptableObjects;
using Game.Scripts.Controller.Dialogue;
using Game.Scripts.Controller.Interact;
using Game.Scripts.Controller.Quest;
using Game.Scripts.Manager.Quest;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Controller.NPC
{
    public class NpcController : Interactable
    {
        [SerializeField]
        private TextMeshProUGUI MissionStatusMark;

        [SerializeField]
        private NpcSO NpcConfig;

        [SerializeField]
        private QuestController QuestController;

        private bool HasQuest => QuestController != null;
        private bool MissionActive;
        private bool QuestStarted;

        protected override void Start()
        {
            base.Start();
            UpdateMissionStatusMark();
        }

        public void UpdateMissionStatusMark()
        {
            if (MissionActive)
            {
                if(QuestStarted)    
                    MissionStatusMark.text = "?";
                else
                    MissionStatusMark.text = "!";
            }
            else
                MissionStatusMark.text = "";
        }

        public void StartQuest()
        {
            MissionActive = true;
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
        }

        protected override void OnPlayerEnter()
        {
            Debug.Log("Player nearby");
        }

        protected override void OnPlayerExit()
        {
            Debug.Log("Player left");
        }

        protected override void OnPlayerInteract()
        {
            Debug.Log("Player interacted");
            
            if(QuestController.HasQuest)

            DialogBoxController.Instance.StartDialog(dialog);
        }

        private Dialog GetDialog()
        {       
            var chosenDialog = new string[0];     
            if(!HasQuest)
            {
                chosenDialog = NpcConfig.StandardDialog;
            }
            else if(QuestController.Completed)
            {
                chosenDialog = NpcConfig.AfterQuestDialog;
            }
            else if(QuestsManager.Instance.CurrentQuest.Id != QuestController.Id)
            {
                chosenDialog = NpcConfig.StandardDialog;
            }
            else if(!QuestController.Started)
            {
                chosenDialog = NpcConfig.StartQuestDialog;
            }
            else if(PlayerController.Instance.ItemHeld == null)
            {   
                chosenDialog = NpcConfig.WaitingEndQuestDialog;
            }
            else if(QuestController. PlayerController.Instance.ItemHeld )
            {
                
            }

            return new Dialog(NpcConfig.name, NpcConfig.Portrait, chosenDialog);
        }
    }
}