using UnityEngine;

namespace Game.Controller.Interactable
{
    [ExecuteInEditMode]
    public abstract class Interactable : MonoBehaviour
    {
        [Range(1, 5f)]
        public float interactableRange = 1f;
        private SphereCollider collider;
        private bool isPlayerInside;

        protected virtual void Start()
        {
            if (gameObject.GetComponent<SphereCollider>() == null)
                gameObject.AddComponent<SphereCollider>();
            collider = GetComponent<SphereCollider>();
            collider.isTrigger = true;
            collider.radius = interactableRange;
        }

        protected virtual void Update()
        {
#if UNITY_EDITOR
            collider.radius = interactableRange;
#endif
            if (isPlayerInside)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                    OnPlayerInteract();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                isPlayerInside = true;
                OnPlayerEnter();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                isPlayerInside = false;
                OnPlayerExit();
            }
        }

        protected abstract void OnPlayerEnter();
        protected abstract void OnPlayerExit();
        protected abstract void OnPlayerInteract();
    }
}