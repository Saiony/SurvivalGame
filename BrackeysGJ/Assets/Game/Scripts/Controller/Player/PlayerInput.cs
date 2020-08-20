using UnityEngine;

namespace Game.Scripts.Controller.Player
{
    public class PlayerInput
    {
        public string Id { get; private set; }
        public KeyCode ButtonCode { get; private set; }
        public Command Command { get; private set; }


        public PlayerInput()
        {
            Id = string.Empty;
            ButtonCode = KeyCode.None;
            Command = null;
        }

        public PlayerInput(string id, KeyCode buttonCode, Command command) : this()
        {
            Id = id;
            ButtonCode = buttonCode;
            Command = command;
        }
    }    
}