using System;
using System.Collections.Generic;
using Game.Controller.Interface.Item;
using Game.ScriptableObject;
using UnityEngine;

namespace Game.Controller.Item
{
    public abstract class Item : MonoBehaviour, ITimeChangeable
    {
        public bool Fowardable => InteractableItem.FowardObject != null;
        public bool Rewindable => InteractableItem.RewindObject != null;

        public InteractableItem InteractableItem;
        public GameObject FowardTime()
        {
            if (Fowardable)
                return InteractableItem.FowardObject;
            throw new ArgumentOutOfRangeException(nameof(InteractableItem.FowardObject));
        }

        public GameObject BackTime()
        {
            if (Rewindable)
                return InteractableItem.RewindObject;
            throw new ArgumentOutOfRangeException(nameof(InteractableItem.RewindObject));
        }
    }
}