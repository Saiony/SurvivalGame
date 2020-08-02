using System;
using System.Collections.Generic;
using Game.Controller.Interface.Item;
using Game.Controller.Item;
using UnityEngine;
using UnityEngine.Events;

namespace Game.ScriptableObject
{
    [CreateAssetMenu(fileName = "InteractableItem", menuName = "ScriptableObjects/InteractableItem", order = 1)]
    public class InteractableItem : UnityEngine.ScriptableObject
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