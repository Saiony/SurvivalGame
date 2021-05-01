
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game.Scripts.Controller.Player
{
    public class InputHandler : MonoBehaviour
    {
        private bool InputBlocked { get; set; }
        public List<PlayerInput> PlayerInputs { get; private set; }

        private PlayerInput Button_W;
        private PlayerInput Button_A;
        private PlayerInput Button_S;
        private PlayerInput Button_D;
        private PlayerInput Button_E;
        private PlayerInput Button_R;
        private PlayerInput Button_F;
        private PlayerInput Button_Space;
        private PlayerInput Button_Esc;
        private PlayerInput Button_I;

        private PlayerInput Button_1;
        private PlayerInput Button_2;
        private PlayerInput Button_3;
        private PlayerInput Button_4;
        private PlayerInput Button_5;

        public static InputHandler Instance;

        private void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);
            else
                Instance = this;
            ConfigureInputs();
        }

        void ConfigureInputs()
        {
            InputBlocked = false;

            Button_W = new PlayerInput("button_w", KeyCode.W, new MoveUpCommand());
            Button_A = new PlayerInput("button_a", KeyCode.A, new MoveLeftCommand());
            Button_S = new PlayerInput("button_s", KeyCode.S, new MoveDownCommand());
            Button_D = new PlayerInput("button_d", KeyCode.D, new MoveRightCommand());

            Button_E = new PlayerInput("button_e", KeyCode.E, new PlowCommand());
            Button_R = new PlayerInput("button_r", KeyCode.R, new PlantCommand());
            Button_F = new PlayerInput("button_f", KeyCode.F, new WaterCommand());

            Button_Space = new PlayerInput("button_space", KeyCode.Space, new InteractCommand());
            Button_Esc = new PlayerInput("button_esc", KeyCode.Escape, new OpenSettingsCommand());
            Button_I = new PlayerInput("button_i", KeyCode.I, new OpenInventoryCommand());

            Button_1 = new PlayerInput("button_1", KeyCode.Alpha1, new SelectQuickItemCommand_1());
            Button_2 = new PlayerInput("button_2", KeyCode.Alpha2, new SelectQuickItemCommand_2());
            Button_3 = new PlayerInput("button_3", KeyCode.Alpha3, new SelectQuickItemCommand_3());
            Button_4 = new PlayerInput("button_4", KeyCode.Alpha4, new SelectQuickItemCommand_4());
            Button_5 = new PlayerInput("button_5", KeyCode.Alpha5, new SelectQuickItemCommand_5());


            PlayerInputs = new List<PlayerInput>();
            PlayerInputs.Add(Button_W);
            PlayerInputs.Add(Button_A);
            PlayerInputs.Add(Button_S);
            PlayerInputs.Add(Button_D);
            PlayerInputs.Add(Button_F);
            PlayerInputs.Add(Button_Space);
            PlayerInputs.Add(Button_Esc);
        }

        public Command HandleInput()
        {
            //Esc and Space command are UNBLOCKABLE
            if (Input.GetKeyDown(Button_Esc.ButtonCode))
                return Button_Esc.Command;
            else if (Input.GetKeyDown(Button_Space.ButtonCode))
                return Button_Space.Command;
            else if (Input.GetKeyDown(Button_I.ButtonCode))
                return Button_I.Command;

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

            else if (Input.GetKeyDown(Button_E.ButtonCode))
                return Button_E.Command;
            else if (Input.GetKeyDown(Button_R.ButtonCode))
                return Button_R.Command;
            else if (Input.GetKeyDown(Button_F.ButtonCode))
                return Button_F.Command;

            //Quick Inventory
            if (Input.GetKeyDown(Button_1.ButtonCode))
                return Button_1.Command;
            else if (Input.GetKeyDown(Button_2.ButtonCode))
                return Button_2.Command;
            else if (Input.GetKeyDown(Button_3.ButtonCode))
                return Button_3.Command;
            else if (Input.GetKeyDown(Button_4.ButtonCode))
                return Button_4.Command;
            else if (Input.GetKeyDown(Button_5.ButtonCode))
                return Button_5.Command;

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

        public bool UpdatePlayerInput(PlayerInput playerInput, KeyCode newKeyCode)
        {
            var oldInput = PlayerInputs.FirstOrDefault(x => x.Id == playerInput.Id);
            if (PlayerInputs.Any(x => x.ButtonCode == newKeyCode))
            {
                Debug.Log("Input already registered");
                return false;
            }

            oldInput.ChangeInputCode(newKeyCode);
            return true;
        }
    }
}