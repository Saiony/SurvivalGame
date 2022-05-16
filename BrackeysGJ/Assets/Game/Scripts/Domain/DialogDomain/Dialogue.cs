using Game.ScriptableObjects;
using Game.Scripts.Controller.UI;
using UnityEngine;

namespace Game.Scripts.Controller.Dialog
{
    [System.Serializable]
    public class Dialogue
    {
        [SerializeField]
        private PortraitSO _portrait = null;
        public PortraitSO Portrait => _portrait;

        [SerializeField]
        [TextArea(3, 10)]
        public string Sentence = null;

        public Dialogue()
        {
            _portrait = null;
            Sentence = string.Empty;
        }

        public Dialogue(string name, PortraitSO portrait, string sentence) : this()
        {
            _portrait = portrait;
            Sentence = sentence;
        }
    }
}
