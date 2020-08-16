using UnityEngine;

namespace Game.Scripts.Controller.Player
{
    public class PlayerInput
    {
        string Id { get; set; }
        KeyCode ButtonCode { get; set; }
        Command Command { get; set; }


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