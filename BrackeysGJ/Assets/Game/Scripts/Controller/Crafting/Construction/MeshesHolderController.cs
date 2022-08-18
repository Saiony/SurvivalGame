using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Controller.Crafting.Construction
{
    public class MeshesHolderController : MonoBehaviour
    {
        [SerializeField]
        List<GameObject> _meshes;
        public List<GameObject> Meshes => _meshes;
    }
}