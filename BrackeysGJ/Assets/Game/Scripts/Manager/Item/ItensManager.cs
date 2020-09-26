using System;
using System.Collections.Generic;
using Game.Scripts.Controller.Item;
using UnityEngine;

namespace Game.Scripts.Manager.Item
{
    public class ItensManager : MonoBehaviour
    {
        [SerializeField]
        private List<ItemController> itens = null;

        public static ItensManager Instance = null;

        void Awake()
        {
            if (Instance != null)
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

                var itemFuturo = item.FutureObject != null ? item.FutureObject.GetComponent<ItemController>() : null;
                var itemPassado = item.PastObject != null ? item.PastObject.GetComponent<ItemController>() : null;

                if (itemFuturo != null)
                {
                    if (itemFuturo.PastObject.name != item.name)
                        throw new ArgumentOutOfRangeException($"{item.name} não possuí {nameof(itemFuturo)}");
                }
                if (itemPassado != null)
                {
                    if (itemPassado.FutureObject.name != item.name)
                        throw new ArgumentOutOfRangeException($"{item.name} não possuí {nameof(itemPassado)}");
                }
            }
        }
    }
}
