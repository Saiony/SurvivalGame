using System;
using System.Collections.Generic;
using DG.Tweening;
using Game.Scripts.Controller.Player;
using Game.Scripts.Controller.Player.Commands;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Controller.Dialog
{
    public class DialogBoxController : MonoBehaviour
    {
        [SerializeField]
        private Image _avatar = null;
        private Image Avatar => _avatar;

        [SerializeField]
        private TextMeshProUGUI _name = null;
        private TextMeshProUGUI Name => _name;

        [SerializeField]
        private TextMeshProUGUI _displayedText = null;
        private TextMeshProUGUI DisplayedText => _displayedText;

        [SerializeField]
        private Image _endDialogIndicator = null;
        private Image EndDialogIndicator => _endDialogIndicator;

        [SerializeField]
        [Range(0.01f, 0.5f)]
        private float _timePerWord = 0;
        private float TimePerWord => _timePerWord;

        private Queue<Dialogue> Dialogues = null;
        private bool DialogActive = false;
        private bool AnimatingText = false;
        private Sequence textAnimation = null;

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
            EndDialogIndicator.DOFade(0, 0);
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
            if (AnimatingText && textAnimation != null)
            {
                //finish animation
                textAnimation.Complete(true);
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
            AnimatingText = true;
            Dialogue newDialogue = Dialogues.Dequeue();
            DisplayedText.text = String.Empty;
            Avatar.sprite = newDialogue.Portrait.Avatar;
            Name.text = newDialogue.Portrait.Name;
            EndDialogIndicator.DOFade(0, 0);

            var textDuration = TimePerWord * newDialogue.Sentence.Length;
            textAnimation = DOTween.Sequence();
            textAnimation.Append(DisplayedText.DOText(newDialogue.Sentence, textDuration));
            textAnimation.Append(EndDialogIndicator.DOFade(1, 0.15f));
            textAnimation.AppendCallback(() => AnimatingText = false);
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
