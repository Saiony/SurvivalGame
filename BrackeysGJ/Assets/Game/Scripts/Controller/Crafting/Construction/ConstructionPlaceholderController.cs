using Game.Scripts.Domain.Items;
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

        BoxCollider _collider;
        public ConstructionStructure Structure { get; private set; }

        public void Init(MeshFilter meshFilter, ConstructionStructure structure)
        {
            Structure = structure;
            SetCollider(Structure.Size);

            _meshFilter.mesh = meshFilter.sharedMesh;
            _renderer.material.color = _defaultColor;
            transform.rotation = Quaternion.identity;

            var meshTransform = meshFilter.gameObject.transform;
            MeshFilter.transform.rotation = meshTransform.rotation;
            MeshFilter.transform.localScale = meshTransform.localScale;
            MeshFilter.transform.localPosition = meshTransform.localPosition;
        }

        public void Rotate(float scrollInput)
        {
            transform.Rotate(Vector3.up * (Mathf.Sign(scrollInput) * 45), Space.Self);
        }

        void Update()
        {
            var overlapSize = Structure.Size / 4;
            var collisions = Physics.OverlapBox(MeshFilter.transform.position, overlapSize);

            if (collisions.Length > 1)
                _renderer.material.color = _blockedColor;
            else
                _renderer.material.color = _defaultColor;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(MeshFilter.transform.position, Structure.Size / 4);
        }

        void SetCollider(Vector3 size)
        {
            if (_collider != null)
            {
                Destroy(_collider);
                _collider = null;
            }

            _collider = gameObject.AddComponent<BoxCollider>();
            _collider.size = size;
            _collider.isTrigger = true;
        }
    }
}