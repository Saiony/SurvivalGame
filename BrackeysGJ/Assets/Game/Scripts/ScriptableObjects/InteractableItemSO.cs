using System;
using UnityEngine;

namespace Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "InteractableItem", menuName = "ScriptableObjects/InteractableItem", order = 1)]
    public class InteractableItemSO : ScriptableObject
    {
        [Header("Passado")]
        public GameObject PastObject;
        public String PastDialogue;


        [Header("Presente")]
        public GameObject PresentObject;
        public String PresentDialog;


        [Header("Futuro")]
        public GameObject FutureObject;
        public String FutureDialogue;
    }
}