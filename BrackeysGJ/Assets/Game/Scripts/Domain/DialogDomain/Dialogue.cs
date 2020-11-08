using Game.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.Controller.Dialog
{
    [System.Serializable]
    public class Dialogue
    {
        public PortraitSO Portrait { get; private set; }

        [SerializeField]
        [TextArea(3, 10)]
        public string Sentence = null;

        public Dialogue()
        {
            Portrait = null;
            Sentence = string.Empty;
        }

        public Dialogue(string name, PortraitSO portrait, string sentence) : this()
        {
            Portrait = portrait;
            Sentence = sentence;
        }
    }
}
