using Game.Scripts.Controller.Player;
using UnityEngine;

namespace Game.Scripts.Controller.Interact
{
    [ExecuteInEditMode]
    public abstract class Interactable : MonoBehaviour
    {
        [Range(1, 5f)]
        public float interactableRange = 1f;
        private SphereCollider Col;


        [SerializeField]
        private bool IsPlayerInside { get; set; }

        [SerializeField]
        private bool IsPlayerTouching { get; set; }

        protected virtual void Start()
        {
            if (gameObject.GetComponent<SphereCollider>() == null)
                gameObject.AddComponent<SphereCollider>();
            Col = GetComponent<SphereCollider>();
            Col.isTrigger = true;
            Col.radius = interactableRange;
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