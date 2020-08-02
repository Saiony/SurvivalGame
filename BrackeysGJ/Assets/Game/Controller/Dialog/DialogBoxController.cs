using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogBoxController : MonoBehaviour
{
    [SerializeField]
    private Image Avatar;
    [SerializeField]
    private TextMeshProUGUI Name;
    [SerializeField]
    private TextMeshProUGUI DisplayedText;

    private Queue<string> Sentences;

    public void StartDialog(Dialog dialog)
    {
        Setup(dialog);
        //TODO: Animação @mike
        DisplayNextSentence();
    }

    private void Setup(Dialog dialog)
    {
        foreach (var sentence in dialog.Sentences)
        {
            Sentences.Enqueue(sentence);
        }

        Name.text = dialog.Name;
        Avatar.sprite = dialog.Portrait;
    }

    private void DisplayNextSentence()
    {
        if (Sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string newSentence = Sentences.Dequeue();
        DisplayedText.text = newSentence;
    }

    private void EndDialog()
    {
        //TODO: Animação @mike    
    }
}
