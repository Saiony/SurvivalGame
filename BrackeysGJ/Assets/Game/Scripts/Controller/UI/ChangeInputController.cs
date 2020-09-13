using System;
using System.Collections;
using Game.Scripts.Controller.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Game.Scripts.Controller.UI
{
    public class ChangeInputController : MonoBehaviour
    {
        [SerializeField]
        private Button readInputButton = null;

        [SerializeField]
        private TextMeshProUGUI inputText = null;

        [SerializeField]
        private TextMeshProUGUI commandText = null;

        private PlayerInput PlayerInput { get; set; }

        private void Awake()
        {
            readInputButton.onClick.AddListener(() => StartCoroutine(ReadInput()));
        }

        public void Setup(PlayerInput playerInput)
        {
            PlayerInput = playerInput;
            inputText.text = playerInput.ButtonCode.ToString();
            commandText.text = playerInput.Command.Name;
        }

        private IEnumerator ReadInput()
        {
            inputText.text = "Press any button";
            Debug.Log("Reading Input...");
            bool done = false;
            while (!done)
            {
                foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        Debug.Log("KeyCode down: " + keyCode);
                        if(InputHandler.Instance.UpdatePlayerInput(PlayerInput, keyCode))
                            inputText.text = keyCode.ToString();
                        done = true;
                    }
                }
                yield return null;
            }
        }
    }
}