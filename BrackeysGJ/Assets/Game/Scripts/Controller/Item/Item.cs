using System;
using System.Collections.Generic;
using Game.Controller.Interface.Item;
using Game.ScriptableObjects;
using UnityEngine;

namespace Game.Controller.Item
{
    public abstract class Item : MonoBehaviour, ITimeChangeable
    {
        public bool Fowardable => InteractableItemSO.FowardObject != null;
        public bool Rewindable => InteractableItemSO.RewindObject != null;
        public InteractableItemSO InteractableItemSO;
        
        public GameObject FowardTime()
        {
            if (Fowardable)
                return InteractableItemSO.FowardObject;
            throw new ArgumentOutOfRangeException(nameof(InteractableItemSO.FowardObject));
        }

        public GameObject RewindTime()
        {
            if (Rewindable)
                return InteractableItemSO.RewindObject;
            throw new ArgumentOutOfRangeException(nameof(InteractableItemSO.RewindObject));
        }
    }
}