using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Controller.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Controller.UI
{
    public class SettingsModalController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _changeInputPrefab = null;
        private GameObject ChangeInputPrefab => _changeInputPrefab;

        private bool Showing { get; set; }

        public static SettingsModalController Instance = null;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            SetupInputPrefabs();
            Hide();
        }

        public void Toggle()
        {
            if (Showing)
                Hide();
            else
                Show();
        }

        private void Show()
        {
            InputHandler.Instance.DisableInput();
            gameObject.SetActive(true);
            Showing = true;
        }

        private void Hide()
        {
            InputHandler.Instance.EnableInput();
            gameObject.SetActive(false);
            Showing = false;
        }

        private void SetupInputPrefabs()
        {
            var playerInputs = InputHandler.Instance.PlayerInputs;
            foreach (var playerInput in playerInputs)
            {
                var changeInputGO = Instantiate(ChangeInputPrefab, Vector3.zero, Quaternion.identity, transform);
                changeInputGO.GetComponent<ChangeInputController>().Setup(playerInput);
            }
        }

        #region ButtonListeners

        #endregion
    }
}