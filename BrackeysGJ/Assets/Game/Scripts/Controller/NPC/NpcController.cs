using Game.ScriptableObjects;
using Game.Scripts.Controller.Dialog;
using Game.Scripts.Controller.Interact;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Controller.NPC
{
    public class NpcController : Interactable
    {
        [SerializeField]
        private TextMeshProUGUI MissionStatusMark;

        [SerializeField]
        private NpcSO npcConfig;

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

            Dialog.Dialog dialog = new Dialog.Dialog(npcConfig.name, npcConfig.Portrait, npcConfig.StandardDialog);
            DialogBoxController.Instance.StartDialog(dialog);
        }
    }
}