using UnityEngine;

namespace BrackeysGJ.Assets.Game.Scripts.Controller.Crafting
{
    public class CraftingInfoController : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup Modal;

        public void Init()
        {
            Hide();
        }

        public void Show(Vector3 pos)
        {
            transform.position = pos;
            Modal.gameObject.SetActive(true);
        }

        public void Hide()
        {
            Modal.gameObject.SetActive(false);
        }
    }
}