using UnityEngine;

namespace Game.Controller.Interact
{
    [ExecuteInEditMode]
    public abstract class Interactable : MonoBehaviour
    {
        [Range(1, 5f)]
        public float interactableRange = 1f;
        private SphereCollider Col;


        [SerializeField]
        private bool IsPlayerInside { get; set;}

        protected virtual void Start()
        {
            if (gameObject.GetComponent<SphereCollider>() == null)
                gameObject.AddComponent<SphereCollider>();
            Col = GetComponent<SphereCollider>();
            Col.isTrigger = true;
            Col.radius = interactableRange;
        }

        protected virtual void LateUpdate()
        {
#if UNITY_EDITOR
            Col.radius = interactableRange;
#endif
            if (IsPlayerInside && !PlayerController.Instance.InputBlocked)
            {
                if(Input.GetKeyUp(KeyCode.Space))
                    OnPlayerInteract();
            }
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

        protected abstract void OnPlayerEnter();
        protected abstract void OnPlayerExit();
        protected abstract void OnPlayerInteract();
    }
}