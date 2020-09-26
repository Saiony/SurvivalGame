using System;
using System.Collections.Generic;
using DG.Tweening;
using Game.Scripts.Controller.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Controller.Dialog
{
    public class DialogBoxController : MonoBehaviour
    {
        [SerializeField]
        private Image Avatar = null;
        [SerializeField]
        private TextMeshProUGUI Name = null;
        [SerializeField]
        private TextMeshProUGUI DisplayedText = null;

        private Queue<Dialogue> Dialogues = null;

        public bool DialogActive = false;

        public static DialogBoxController Instance = null;

        private Action EndDialogCallback = null;

        void Awake()
        {
            if (!Instance)
                Instance = this;
            else
                Destroy(gameObject);
            gameObject.SetActive(false);
        }

        public void StartDialog(Dialogue[] dialogues, Action callback)
        {
            if (dialogues == null)
                return;
            InputHandler.Instance.DisableInput();
            EndDialogCallback = callback;
            Setup(dialogues);
            //TODO: Animação @mike
            DialogActive = true;
            gameObject.SetActive(true);
            DisplayNextDialogue();
        }

        public void Interact(Dialogue[] dialogue)
        {
            if (!DialogActive)
            {
                StartDialog(dialogue, () =>
                {
                    Debug.Log("Dialog Ended");
                });
                return;
            }
            DisplayNextDialogue();
        }

        private void Setup(Dialogue[] dialogues)
        {
            Dialogues = new Queue<Dialogue>();
            foreach (var dialog in dialogues)
            {
                Dialogues.Enqueue(dialog);
            }
        }

        private void DisplayNextDialogue()
        {
            if (Dialogues.Count == 0)
            {
                EndDialog();
                return;
            }

            Dialogue newDialogue = Dialogues.Dequeue();
            DisplayedText.text = String.Empty;
            Avatar.sprite = newDialogue.Portrait.Avatar;
            Name.text = newDialogue.Portrait.Name;

            DisplayedText.DOText(newDialogue.Sentence, 1f);
        }

        private void EndDialog()
        {
            //TODO: Animação @mike
            InputHandler.Instance.EnableInput();
            DialogActive = false;
            EndDialogCallback?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
