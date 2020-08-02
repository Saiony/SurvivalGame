using Game.Controller.Interact;
using TMPro;
using UnityEngine;
namespace Game.Controller.NPC
{
    public class NpcController : Interactable
    {
        public TextMeshProUGUI missionStatusMark;

        private bool hasActiveMission;
        private bool isWaitingPlayerReturnFromQuest;

        void Start()
        {
            base.Start();
            UpdateMissionStatusMark();
        }

        public void UpdateMissionStatusMark()
        {
            if (hasActiveMission)
            {
                missionStatusMark.text = "!";
            }
        }

        public void LetPlayerStartQuestion()
        {
            this.hasActiveMission = true;
        }

        void Update()
        {
            base.Update();
        }

        protected override void OnPlayerEnter()
        {
        }

        protected override void OnPlayerExit()
        {
        }

        protected override void OnPlayerInteract()
        {
            if (!hasActiveMission)
                return;
        }
    }
}