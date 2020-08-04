using System;
using System.Collections.Generic;
using Game.Scripts.Controller.Item;
using UnityEngine;

namespace Game.Scripts.Manager.Item
{
    public class ItensManager : MonoBehaviour
    {
        [SerializeField]
        private List<Controller.Item.Item> itens;

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
            if (itens == null)
                throw new ArgumentOutOfRangeException(nameof(itens));
            foreach (var item in itens)
            {

                var itemFuturo = item.InteractableItemSO.FutureObject != null ? item.InteractableItemSO.FutureObject.GetComponent<ItemInteractableController>() : null;
                var itemPassado = item.InteractableItemSO.PastObject != null ? item.InteractableItemSO.PastObject.GetComponent<ItemInteractableController>() : null;

                if (itemFuturo != null)
                {
                    if (itemFuturo.InteractableItemSO.PastObject.name != item.name)
                        throw new ArgumentOutOfRangeException($"{item.InteractableItemSO.name} não possuí {nameof(itemFuturo)}");
                }
                if (itemPassado != null)
                {
                    if (itemPassado.InteractableItemSO.FutureObject.name != item.name)
                        throw new ArgumentOutOfRangeException($"{item.InteractableItemSO.name} não possuí {nameof(itemPassado)}");
                }
            }
        }
    }
}
