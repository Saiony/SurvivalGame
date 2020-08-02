using System.Collections.Generic;
using Game.Controller.Interface.Item;
using Game.ScriptableObject;
using UnityEngine;

namespace Game.Controller.Item
{
    public abstract class Item : MonoBehaviour, ITimeChangeable
    {
        public bool Fowardable => InteractableItem.Futuro != null;
        public bool Rewindable => InteractableItem.Passado != null;

        public InteractableItem InteractableItem;
        public GameObject FowardTime()
        {
            if (InteractableItem.Futuro)
                return InteractableItem.Futuro;
            return gameObject;
        }

        public GameObject BackTime()
        {
            if (InteractableItem.Passado)
                return InteractableItem.Passado;
            return gameObject;
        }
    }
}