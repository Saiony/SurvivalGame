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

        public void Interact(Vector3 pos)
        {
            OnInteract(pos);
        }

        public void Plow(Vector3 pos)
        {
            OnPlow(pos);
        }

        public void Water(Vector3 pos)
        {
            OnWater(pos);
        }

        public void Plant(Vector3 pos)
        {
            OnPlant(pos);
        }

        public void Chop(Vector3 pos)
        {
            OnChop(pos);
        }

        protected abstract void OnPlayerEnter();
        protected abstract void OnPlayerExit();
        protected abstract void OnInteract(Vector3 pos);

        protected virtual void OnPlow(Vector3 pos)
        {
        }
        protected virtual void OnWater(Vector3 pos)
        {
        }
        protected virtual void OnPlant(Vector3 pos)
        {
        }
        protected virtual void OnChop(Vector3 pos)
        {
        }
    }
}