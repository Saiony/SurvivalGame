using System;
using System.Collections;
using Game.Scripts.Controller.Player;
using Game.Scripts.Controller.Player.Commands;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Game.Scripts.Controller.UI
{
    public class ChangeInputController : MonoBehaviour
    {
        [SerializeField]
        private Button _readInputButton = null;
        private Button ReadInputButton => _readInputButton;

        [SerializeField]
        private TextMeshProUGUI _inputText = null;
        private TextMeshProUGUI InputText => _inputText;

        [SerializeField]
        private TextMeshProUGUI _commandText = null;
        private TextMeshProUGUI CommandText => _commandText;

        private PlayerInput PlayerInput { get; set; }

        private void Awake()
        {
            ReadInputButton.onClick.AddListener(() => StartCoroutine(ReadInput()));
        }

        public void Setup(PlayerInput playerInput)
        {
            PlayerInput = playerInput;
            InputText.text = playerInput.ButtonCode.ToString();
            CommandText.text = playerInput.Command.Name;
        }

        private IEnumerator ReadInput()
        {
            InputText.text = "Press any button";
            Debug.Log("Reading Input...");
            bool done = false;
            while (!done)
            {
                foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        Debug.Log("KeyCode down: " + keyCode);
                        if (InputHandler.Instance.UpdatePlayerInput(PlayerInput, keyCode))
                            InputText.text = keyCode.ToString();
                        done = true;
                    }
                }
                yield return null;
            }
        }
    }
}