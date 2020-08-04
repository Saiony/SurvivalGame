using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Controller.Dialogue
{
    public class DialogBoxController : MonoBehaviour
    {
        [SerializeField]
        private Image Avatar;
        [SerializeField]
        private TextMeshProUGUI Name;
        [SerializeField]
        private TextMeshProUGUI DisplayedText;

        private Queue<string> Sentences;

        private bool DialogActive = false;

        public static DialogBoxController Instance = null;

        void Awake()
        {
            if (!Instance)
                Instance = this;
            else
                Destroy(gameObject);
            gameObject.SetActive(false);
        }

        public void StartDialog(Dialog dialog)
        {
            PlayerController.Instance.DisableInput();
            Setup(dialog);
            //TODO: Animação @mike
            DialogActive = true;
            gameObject.SetActive(true);
            DisplayNextSentence();
        }

        private void Setup(Dialog dialog)
        {
            Sentences = new Queue<string>();
            foreach (var sentence in dialog.Sentences)
            {
                Sentences.Enqueue(sentence);
            }

            Name.text = dialog.Name;
            Avatar.sprite = dialog.Portrait;
        }

        private void LateUpdate()
        {
            if (DialogActive && Input.GetKeyUp(KeyCode.Space))
            {
                DisplayNextSentence();
            }
        }

        private void DisplayNextSentence()
        {
            if (Sentences.Count == 0)
            {
                EndDialog();
                return;
            }

            string newSentence = Sentences.Dequeue();
            DisplayedText.text = String.Empty;
            DisplayedText.DOText(newSentence, 0.5f);
        }

        private void EndDialog()
        {
            //TODO: Animação @mike
            PlayerController.Instance.EnableInput();
            DialogActive = false;
            gameObject.SetActive(false);
        }
    }
}
