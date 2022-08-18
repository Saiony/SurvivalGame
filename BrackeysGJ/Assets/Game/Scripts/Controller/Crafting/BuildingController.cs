using Game.Scripts.Controller.Crafting.Construction;
using UnityEngine;

namespace Game.Scripts.Controller.Crafting
{
    public class BuildingController : MonoBehaviour
    {
        [SerializeField]
        MeshesHolderController _meshesHolder;
        public MeshesHolderController MeshesHolder => _meshesHolder;
    }
}