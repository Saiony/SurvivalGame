using System;
using Game.ScriptableObjects;
using Game.Scripts.Controller.Dialog;
using Game.Scripts.Controller.Interact;
using Game.Scripts.Controller.Player;
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

        protected override void Start()
        {
            base.Start();
            UpdateMissionStatusMark();
        }

        public void UpdateMissionStatusMark()
        {
            if (QuestController.Started)
            {
                if (QuestController.Started)
                    MissionStatusMark.text = "?";
                else
                    MissionStatusMark.text = "!";
            }
            else
                MissionStatusMark.text = "";
        }

        public void StartQuest()
        {            
            QuestController.StartQuest();
        }

        private void Finishquest()
        {
            //QuestsManager.Instance.FinishQuest(QuestController);
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

            var chosenDialog = GetDialog();
            DialogBoxController.Instance.StartDialog(chosenDialog, () => 
            {
                Debug.Log("Dialog Ended");
            });
        }

        protected override void OnPlayerUse()
        {
            OnPlayerInteract();
        }

        private Dialogue[] GetDialog()
        {
            var chosenDialog = new Dialogue[0];
            if (!HasQuest)
            {
                chosenDialog = NpcConfig.StandardDialog;
            }
            else if (QuestController.Completed)
            {
                chosenDialog = NpcConfig.AfterQuestDialog;
            }
            else if (QuestsManager.Instance.CurrentQuest.Name != QuestController.Name)
            {
                chosenDialog = NpcConfig.StandardDialog;
            }
            else if (!QuestController.Started)
            {
                chosenDialog = NpcConfig.StartQuestDialog;
                StartQuest();
            }
            else if (PlayerController.Instance.ItemHeld == null)
            {
                chosenDialog = NpcConfig.WaitingEndQuestDialog;
            }
            else if (QuestController.ReceiveItem(PlayerController.Instance.ItemHeld))
            {
                //Feedback positivo de UI
                Debug.Log("O NPC gosta do que você fez pq vc tem cheiro de monange");
                PlayerController.Instance.GiveItemHeld();

                if (QuestController.Completed)
                {
                    chosenDialog = NpcConfig.EndQuestDialog;
                    Finishquest();
                }
            }
            else
            {
                //Feedback negativo de UI
                PlayerController.Instance.GiveItemHeld();
                Debug.Log("O NPC não gosta do que você fez pq você fede");
            }

            return chosenDialog;
        }
    }
}