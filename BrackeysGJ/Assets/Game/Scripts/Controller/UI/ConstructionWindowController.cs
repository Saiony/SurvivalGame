using UnityEngine;

namespace Game.Scripts.Controller.UI
{
    public class ConstructionWindowController : MonoBehaviour
    {
        [SerializeField]
        private ConstructionUIController _constructionController;

        public void Init()
        {
            _constructionController.Init(); 
        }

        public void Show()
        {
            _constructionController.Show();
        }

        public void Hide()
        {
            _constructionController.Hide();
        }
    }
}