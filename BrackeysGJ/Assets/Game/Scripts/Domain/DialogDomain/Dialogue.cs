using UnityEngine;

namespace Game.Scripts.Controller.Dialog
{
    public class Dialogue
    {
        public string Name;
        public Sprite Portrait;

        [TextArea(3, 10)]
        public string [] Sentences;

        public Dialogue()
        {
            Name = string.Empty;
            Portrait = null;
            Sentences = new string[0];
        }

        public Dialogue(string name, Sprite portrait, string[] sentences) : this()
        {
            Name = name;
            Portrait = portrait;
            Sentences = sentences;
        }
    }
}
