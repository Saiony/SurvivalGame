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
                if (QuestStarted)
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

            DialogBoxController.Instance.StartDialog(GetDialog());
        }

        private Dialogue.Dialog GetDialog()
        {
            var chosenDialog = new string[0];
            if (!HasQuest)
            {
                chosenDialog = NpcConfig.StandardDialog;
            }
            else if (QuestController.Completed)
            {
                chosenDialog = NpcConfig.AfterQuestDialog;
            }
            else if (QuestsManager.Instance.CurrentQuest.Id != QuestController.Id)
            {
                chosenDialog = NpcConfig.StandardDialog;
            }
            else if (!QuestController.Started)
            {
                chosenDialog = NpcConfig.StartQuestDialog;
            }
            else if (PlayerController.Instance.ItemHeld == null)
            {
                chosenDialog = NpcConfig.WaitingEndQuestDialog;
            }
            else if (QuestController.ReceiveItem(PlayerController.Instance.ItemHeld))
            {
                //Feedback positivo de UI
                Debug.Log("O NPC gosta do que você fez pq vc tem cheiro de monange");

                if (QuestController.Completed)
                    chosenDialog = NpcConfig.EndQuestDialog;
            }
            else
            {
                //Feedback negativo de UI
                Debug.Log("O NPC não gosta do que você fez pq você fede");
            }
            chosenDialog = NpcConfig.StandardDialog;
            return new Dialogue.Dialog(NpcConfig.name, NpcConfig.Portrait, chosenDialog);
        }
    }
}