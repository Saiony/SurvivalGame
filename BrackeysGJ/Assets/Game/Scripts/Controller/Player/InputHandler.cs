
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game.Scripts.Controller.Player
{
    public class InputHandler : MonoBehaviour
    {
        public bool InputBlocked { get; private set; }

        private List<PlayerInput> PlayerInputs { get; set; }

        private PlayerInput Button_W;
        private PlayerInput Button_A;
        private PlayerInput Button_S;
        private PlayerInput Button_D;
        private PlayerInput Button_F;
        private PlayerInput Button_Space;
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

            Button_W = new PlayerInput("button_w", KeyCode.W, new MoveUpCommand());
            Button_A = new PlayerInput("button_a", KeyCode.A, new MoveLeftCommand());
            Button_S = new PlayerInput("button_s", KeyCode.S, new MoveDownCommand());
            Button_D = new PlayerInput("button_d", KeyCode.D, new MoveRightCommand());
            Button_F = new PlayerInput("button_f", KeyCode.F, new TimeTravelCommand());
            Button_Space = new PlayerInput("button_space", KeyCode.Space, new InteractCommand());

            PlayerInputs.Add(Button_W);
            PlayerInputs.Add(Button_A);
            PlayerInputs.Add(Button_S);
            PlayerInputs.Add(Button_D);
            PlayerInputs.Add(Button_F);
            PlayerInputs.Add(Button_Space);
        }

        public Command HandleInput()
        {
            if (InputBlocked)
                return null;
            if (Input.GetKey(Button_W.ButtonCode))
                return Button_W.Command;
            if (Input.GetKey(Button_A.ButtonCode))
                return Button_A.Command;
            else if (Input.GetKey(Button_S.ButtonCode))
                return Button_S.Command;
            else if (Input.GetKey(Button_D.ButtonCode))
                return Button_D.Command;
            else if (Input.GetKey(Button_F.ButtonCode))
                return Button_F.Command;
            else if (Input.GetKey(Button_Space.ButtonCode))
                return Button_Space.Command;
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

        public void UpdateInput(string newKeyCode)
        {
            // var oldPlayerInput = Button_F;
            // var inputzin = PlayerInputs.FirstOrDefault(x => x.Id == oldPlayerInput.Id);
            // inputzin = new PlayerInput(inputzin.Id, newKeyCode, inputzin.Command);
        }
    }
}