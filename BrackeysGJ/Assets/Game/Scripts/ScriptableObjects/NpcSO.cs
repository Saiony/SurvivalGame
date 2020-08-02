using UnityEngine;

namespace Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NPC", menuName = "ScriptableObjects/NPC", order = 1)]
    public class NpcSO : ScriptableObject
    {
        public string Name;
        public Sprite Portrait;

        [TextArea(3, 10)]
        public string[] StandardDialog;

        [TextArea(3, 10)]
        public string[] StartQuestDialog;

        [TextArea(3, 10)]
        public string[] WaitingEndQuestDialog;

        [TextArea(3, 10)]
        public string[] EndQuestDialog;

        [TextArea(3, 10)]
        public string[] AfterQuestDialog;
    }
}