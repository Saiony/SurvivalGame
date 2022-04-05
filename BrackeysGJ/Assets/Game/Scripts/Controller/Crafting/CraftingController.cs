using Game.Scripts.ScriptableObjects.Crafting;
using UnityEngine;

namespace BrackeysGJ.Assets.Game.Scripts.Controller.Crafting
{
    public class CraftingController : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup Content;

        [SerializeField]
        private CraftingInfoController CraftingInfo;

        [SerializeField]
        private CraftingListSO CraftingList;  

        private void Start() 
        {
            CraftingInfo.Init();
        }

        public void Show()
        {
            Content.gameObject.SetActive(true);
        }

        public void Hide()
        {
            Content.gameObject.SetActive(false);
        }
    }
}