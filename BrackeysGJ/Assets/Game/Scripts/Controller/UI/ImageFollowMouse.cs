using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Controller.UI
{
    public class ImageFollowMouse : MonoBehaviour
    {
        [SerializeField]
        private Image _image = null;
        private Image Image => _image;

        private Vector3 Offset { get; set; }
        private Camera MainCamera { get; set; }

        private void Start()
        {
            MainCamera = Camera.main;
            Offset = new Vector3(20, -20, 0);
        }

        void Update()
        {
            transform.position = Input.mousePosition + Offset;
        }

        public void Show(Sprite sprite)
        {
            Image.sprite = sprite;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}


