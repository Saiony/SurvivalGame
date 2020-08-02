using System;
using UnityEngine;

namespace Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "InteractableItem", menuName = "ScriptableObjects/InteractableItem", order = 1)]
    public class InteractableItemSO : ScriptableObject
    {
        [Header("Passado")]
        public GameObject RewindObject;
        public String DialogueOnRewind;

        [Header("Futuro")]
        public GameObject FowardObject;
        public String DialogueOnFoward;

        [Header("Presente")]
        public GameObject Present;
    }
}