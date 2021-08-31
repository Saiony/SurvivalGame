using System;
using Game.ScriptableObjects;
using Game.Scripts.Controller.Dialog;
using Game.Scripts.Controller.Player;
using Game.Scripts.Controller.Quest;
using Game.Scripts.Manager.Quest;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Controller.NPC
{
    public class NpcController : MonoBehaviour, ITalkable
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

        [SerializeField]
        private Collider _detectionCollider = null;
        public Collider DetectionCollider => _detectionCollider;

        private bool HasQuest => QuestController != null;

        private void Start()
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

        public void OnTalk()
        {
            Debug.Log("Player interacted");
            DialogBoxController.Instance.Interact(GetDialog());
        }

        //Alguém refatora isso pfvr
        private Dialogue[] GetDialog()
        {
            var chosenDialog = new Dialogue[0];
            chosenDialog = NpcConfig.StandardDialog;

            return chosenDialog;
        }
    }
}