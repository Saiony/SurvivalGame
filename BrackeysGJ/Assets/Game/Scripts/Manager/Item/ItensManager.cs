using System;
using System.Collections.Generic;
using System.Linq;
using Game.ScriptableObjects;
using Game.Scripts.Controller.Item;
using UnityEngine;

namespace Game.Scripts.Manager.Item
{
    public class ItensManager : MonoBehaviour
    {
        [SerializeField]
        private List<InteractableItemSO>  interactableItemsSO;

        public static ItensManager Instance = null;

        void Awake()
        {
            if(Instance != null)
                throw new Exception("Singleton already populated");
            Instance = this;
        }

        void Start()
        {
            ValidarExistenciaDosItens();
        }

        private void ValidarExistenciaDosItens()
        {
            if (interactableItemsSO == null)
                throw new ArgumentOutOfRangeException(nameof(interactableItemsSO));
            foreach (var item in interactableItemsSO)
            {
                var itemFuturo = item.FutureObject != null ? item.FutureObject.GetComponent<ItemInteractableController>() : null;
                var itemPassado = item.PastObject != null ? item.PastObject.GetComponent<ItemInteractableController>() : null;
                var itemPresente = item.PresentObject != null ? item.PresentObject.GetComponent<ItemInteractableController>() : null;
                if (itemFuturo != null)
                {
                    if (itemFuturo.InteractableItemSO.PastObject.name != this.gameObject.name)
                        throw new ArgumentOutOfRangeException(nameof(itemFuturo));
                }
                if (itemPassado != null)
                {
                    if (itemPassado.InteractableItemSO.FutureObject.name != this.gameObject.name)
                        throw new ArgumentOutOfRangeException(nameof(itemPassado));
                }

                if (itemPresente == null)
                {
                    throw new ArgumentOutOfRangeException($"{item.name} deveria estar populado.");
                }
            }
        }
    }
}
