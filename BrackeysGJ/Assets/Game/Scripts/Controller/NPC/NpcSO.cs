using Game.Scripts.Controller.Dialog;
using UnityEngine;

namespace Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NPC", menuName = "ScriptableObjects/NPC", order = 1)]
    public class NpcSO : ScriptableObject
    {
        public Dialogue[] StandardDialog;

        public Dialogue[] StartQuestDialog;

        public Dialogue[] WaitingEndQuestDialog;

        public Dialogue[] EndQuestDialog;

        public Dialogue[] AfterQuestDialog;
    }
}