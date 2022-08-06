using UnityEngine;

namespace Game.Scripts.Controller.Crafting.Construction
{
    public class ConstructionPlaceholderController : MonoBehaviour
    {
        [SerializeField]
        MeshFilter _meshFilter;
        public MeshFilter MeshFilter => _meshFilter;

        public void SetMesh(Mesh mesh)
        {
            _meshFilter.mesh = mesh;
        }
    }
}