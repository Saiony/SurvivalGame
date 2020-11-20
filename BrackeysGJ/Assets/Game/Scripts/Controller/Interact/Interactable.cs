using Game.Scripts.Controller.Player;
using UnityEngine;

namespace Game.Scripts.Controller.Interact
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField]
        [Range(1, 5f)]
        private float _interactableRange = 0;
        public float interactableRange => _interactableRange;

        [SerializeField]
        private Collider _detectionCollider = null;
        protected Collider DetectionCollider => _detectionCollider;

        private bool IsPlayerInside { get; set; }

        private void Start()
        {
            DetectionCollider.isTrigger = true;

            OnDidStart();
        }

        protected virtual void OnDidStart()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                IsPlayerInside = true;
                OnPlayerEnter();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                IsPlayerInside = false;
                OnPlayerExit();
            }
        }

        public void Interact()
        {
            OnPlayerInteract();
        }

        protected abstract void OnPlayerEnter();
        protected abstract void OnPlayerExit();
        protected abstract void OnPlayerInteract();
    }
}