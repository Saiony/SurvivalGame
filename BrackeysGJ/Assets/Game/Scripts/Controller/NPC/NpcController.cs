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
        private TextMeshProUGUI _missionStatusMark = null;
        private TextMeshProUGUI MissionStatusMark => _missionStatusMark;

        [SerializeField]
        private NpcSO _npcConfig = null;
        private NpcSO NpcConfig => _npcConfig;

        [SerializeField]
        private QuestController _questController = null;
        private QuestController QuestController => _questController;

        private bool HasQuest => QuestController != null;

        protected override void OnDidStart()
        {
            if (QuestController != null)
                UpdateMissionStatusMark();
        }

        public void UpdateMissionStatusMark()
        {
            if (QuestsManager.Instance.IsActiveQuest(QuestController.Name))
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

        protected override void OnPlayerEnter()
        {
            Debug.Log("Player nearby");
        }

        protected override void OnPlayerExit()
        {
            Debug.Log("Player left");
        }

        protected override void OnInteract(Vector3 pos)
        {
            Debug.Log("Player interacted");
            DialogBoxController.Instance.Interact(GetDialog());
        }

        //Alguém refatora isso pfvr
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
            else if (!QuestsManager.Instance.IsActiveQuest(QuestController.Name))
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