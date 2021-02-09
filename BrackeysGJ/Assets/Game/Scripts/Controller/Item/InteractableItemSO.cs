using System;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "InteractableItem", menuName = "ScriptableObjects/InteractableItem", order = 1)]
    public class InteractableItemSO : ScriptableObject
    {
        [Header("Passado")]
        public GameObject PastObject;
        [TextArea(3, 10)]
        public String PastDialogue;


        [Header("Presente")]
        [TextArea(3, 10)]
        public String PresentDialog;


        [Header("Futuro")]
        public GameObject FutureObject;
        [TextArea(3, 10)]
        public String FutureDialogue;
    }
}