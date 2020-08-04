using UnityEngine;

namespace Game.Scripts.Controller.Dialog
{
    public class Dialog
    {
        public string Name;
        public Sprite Portrait;

        [TextArea(3, 10)]
        public string [] Sentences;

        public Dialog(string name, Sprite portrait, string[] sentences)
        {
            Name = name;
            Portrait = portrait;
            Sentences = sentences;
        }
    }
}
