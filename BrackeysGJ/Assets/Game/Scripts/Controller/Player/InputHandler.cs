
using System;
using UnityEngine;

namespace Game.Scripts.Controller.Player
{
    public class InputHandler : MonoBehaviour
    {
        public bool InputBlocked { get; private set; }
        
        private KeyCode Button_W;
        private KeyCode Button_A;
        private KeyCode Button_S;
        private KeyCode Button_D;
        private KeyCode Button_F;
        private KeyCode Button_Space;

        private Command Command_W;
        private Command Command_A;
        private Command Command_S;
        private Command Command_D;
        private Command Command_F;
        private Command Command_Space;

        public static InputHandler Instance;

        private void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);
            else
                Instance = this;
        }

        void Start()
        {
            InputBlocked = false;

            Button_W = KeyCode.W;
            Button_A = KeyCode.A;
            Button_S = KeyCode.S;
            Button_D = KeyCode.D;
            Button_F = KeyCode.F;
            Button_Space = KeyCode.Space;

            Command_W = new MoveUpCommand();
            Command_A = new MoveLeftCommand();
            Command_S = new MoveDownCommand();
            Command_D = new MoveRightCommand();
            Command_F = new TimeTravelCommand();
            Command_Space = new InteractCommand();
        }

        public Command HandleInput()
        {
            if (InputBlocked)
                return null;
            if (Input.GetKey(Button_F))
                return Command_F;
            if (Input.GetKey(Button_W))
                return Command_W;
            else if (Input.GetKey(Button_A))
                return Command_A;
            else if (Input.GetKey(Button_S))
                return Command_S;
            else if (Input.GetKey(Button_D))
                return Command_D; 

            return null;
        }

        public void DisableInput()
        {
            InputBlocked = true;
        }

        public void EnableInput()
        {
            InputBlocked = false;
        }

        public void UpdateInput(Command command, KeyCode keyCode)
        {
            
        }
    }
}