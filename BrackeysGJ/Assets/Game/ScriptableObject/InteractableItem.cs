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
        public GameObject Passado;
        public GameObject Presente;
        public GameObject Futuro;
    }
}