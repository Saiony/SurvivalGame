using Game.Scripts.Controller.Construction;
using UnityEngine;

namespace Game.Scripts.Controller.Crafting.Construction
{
    public class ConstructionWindowController : MonoBehaviour
    {
        [SerializeField]
        private ConstructionController _constructionController;

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