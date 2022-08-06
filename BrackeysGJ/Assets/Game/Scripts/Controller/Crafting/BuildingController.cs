using UnityEngine;

namespace Game.Scripts.Controller.Crafting
{
    public class BuildingController : MonoBehaviour
    {
        [SerializeField]
        MeshFilter _meshFilter;
        public MeshFilter MeshFilter => _meshFilter;
    }
}