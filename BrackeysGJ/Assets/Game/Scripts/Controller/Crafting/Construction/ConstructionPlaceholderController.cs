using UnityEngine;

namespace Game.Scripts.Controller.Crafting.Construction
{
    public class ConstructionPlaceholderController : MonoBehaviour
    {
        [SerializeField]
        MeshFilter _meshFilter;
        public MeshFilter MeshFilter => _meshFilter;

        [SerializeField]
        Color _defaultColor;

        [SerializeField]
        Color _blockedColor;

        [SerializeField]
        Renderer _renderer;

        public void SetMesh(Mesh mesh)
        {
            _meshFilter.mesh = mesh;
            _renderer.material.color = _defaultColor;
        }

        private void OnTriggerEnter(Collider other)
        {
            _renderer.material.color = _blockedColor;
        }

        private void OnTriggerExit(Collider other)
        {
            _renderer.material.color = _defaultColor;
        }
    }
}