using System;
using Game.ScriptableObjects;
using Game.Scripts.Controller.Interface.Item;
using UnityEngine;

namespace Game.Scripts.Controller.Item
{
    public abstract class Item : MonoBehaviour, ITimeChangeable
    {
        public bool Fowardable => InteractableItemSO.FutureObject != null;
        public bool Rewindable => InteractableItemSO.PastObject != null;
        public InteractableItemSO InteractableItemSO;
        
        public GameObject FowardTime()
        {
            if (Fowardable)
                return InteractableItemSO.FutureObject;
            throw new ArgumentOutOfRangeException(nameof(InteractableItemSO.FutureObject));
        }

        public GameObject RewindTime()
        {
            if (Rewindable)
                return InteractableItemSO.PastObject;
            throw new ArgumentOutOfRangeException(nameof(InteractableItemSO.PastObject));
        }
    }
}