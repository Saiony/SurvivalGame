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

        protected virtual void LateUpdate()
        {
#if UNITY_EDITOR
            Col.radius = interactableRange;
#endif
            if (IsPlayerTouching /*&& !PlayerController.Instance.InputBlocked*/)
            {
                if (Input.GetKeyUp(KeyCode.Space))
                    OnPlayerInteract();
                if (Input.GetKeyDown(KeyCode.F))
                    OnPlayerUse();
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

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Player")
            {
                IsPlayerTouching = true;
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.tag == "Player")
            {
                IsPlayerTouching = false;
            }
        }

        protected abstract void OnPlayerEnter();
        protected abstract void OnPlayerExit();
        protected abstract void OnPlayerInteract();
        protected abstract void OnPlayerUse();
    }
}