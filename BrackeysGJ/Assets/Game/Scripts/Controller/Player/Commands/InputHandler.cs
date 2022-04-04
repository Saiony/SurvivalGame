
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

        private PlayerInput Button_Space;
        private PlayerInput Button_Esc;
        private PlayerInput Button_Tab;

        private PlayerInput Button_Mouse_Left;
        private PlayerInput Button_Left_Shift;
        private PlayerInput Button_Left_Shift_Released;

        private PlayerInput Button_1;
        private PlayerInput Button_2;
        private PlayerInput Button_3;
        private PlayerInput Button_4;
        private PlayerInput Button_5;

        private List<Command> Inputs { get; set; }
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
            Inputs = new List<Command>();
            InputBlocked = false;

            Button_W = new PlayerInput("button_w", KeyCode.W, new MoveCommand());
            Button_A = new PlayerInput("button_a", KeyCode.A, new MoveCommand());
            Button_S = new PlayerInput("button_s", KeyCode.S, new MoveCommand());
            Button_D = new PlayerInput("button_d", KeyCode.D, new MoveCommand());

            Button_Space = new PlayerInput("button_space", KeyCode.Space, new InteractCommand());
            Button_Esc = new PlayerInput("button_esc", KeyCode.Escape, new OpenSettingsCommand());
            Button_Tab = new PlayerInput("button_tab", KeyCode.Tab, new OpenInventoryCommand());

            Button_Mouse_Left = new PlayerInput("button_mouse_left", KeyCode.Mouse0, new RightArmCommand());
            Button_Left_Shift = new PlayerInput("button_left_shift", KeyCode.LeftShift, new RunCommand());
            Button_Left_Shift_Released = new PlayerInput("button_left_shift_released", KeyCode.LeftShift, new StopRunningCommand());

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

            PlayerInputs.Add(Button_Space);
            PlayerInputs.Add(Button_Esc);
            PlayerInputs.Add(Button_Tab);

            PlayerInputs.Add(Button_Mouse_Left);
        }

        public List<Command> HandleInput()
        {
            Inputs.Clear();

            //UNBLOCKABLE commands
            if (Input.GetKeyDown(Button_Esc.ButtonCode))
                Inputs.Add(Button_Esc.Command);
            else if (Input.GetKeyDown(Button_Space.ButtonCode))
                Inputs.Add(Button_Space.Command);
            else if (Input.GetKeyDown(Button_Tab.ButtonCode))
                Inputs.Add(Button_Tab.Command);

            if (InputBlocked)
                return Inputs;
            if (Input.GetKeyDown(Button_Mouse_Left.ButtonCode))
                Inputs.Add(Button_Mouse_Left.Command);
            else if (Input.GetKey(Button_W.ButtonCode) || Input.GetKey(Button_A.ButtonCode) 
                     || Input.GetKey(Button_S.ButtonCode) || Input.GetKey(Button_D.ButtonCode))
                Inputs.Add(Button_W.Command);

            //Run
            if(Input.GetKeyDown(Button_Left_Shift.ButtonCode))
                Inputs.Add(Button_Left_Shift.Command);
            else if(Input.GetKeyUp(Button_Left_Shift_Released.ButtonCode))
                Inputs.Add(Button_Left_Shift_Released.Command);

            //Quick Inventory
            if (Input.GetKeyDown(Button_1.ButtonCode))
                Inputs.Add(Button_1.Command);
            else if (Input.GetKeyDown(Button_2.ButtonCode))
                Inputs.Add(Button_2.Command);
            else if (Input.GetKeyDown(Button_3.ButtonCode))
                Inputs.Add(Button_3.Command);
            else if (Input.GetKeyDown(Button_4.ButtonCode))
                Inputs.Add(Button_4.Command);
            else if (Input.GetKeyDown(Button_5.ButtonCode))
                Inputs.Add(Button_5.Command);

            return Inputs;
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