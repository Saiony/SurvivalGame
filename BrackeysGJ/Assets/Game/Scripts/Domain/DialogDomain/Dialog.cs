using UnityEngine;

namespace Game.Scripts.Controller.Dialogue
{
    public class Dialog
    {
        public string Name;
        public Sprite Portrait;

        [TextArea(3, 10)]
        public string [] Sentences;

        public Dialog()
        {
            Name = string.Empty;
            Portrait = null;
            Sentences = new string[0];
        }

        public Dialog(string name, Sprite portrait, string[] sentences) : this()
        {
            Name = name;
            Portrait = portrait;
            Sentences = sentences;
        }
    }
}
