using System.Collections.Generic;
using Game.Scripts.Domain.Items;
using UnityEngine;

namespace Game.Scripts.Controller.Crafting.Construction
{
    public class ConstructionPlaceholderController : MonoBehaviour
    {
        [SerializeField]
        Color _defaultColor;

        [SerializeField]
        Color _blockedColor;

        BoxCollider _collider;
        public ConstructionStructure Structure { get; private set; }
        public MeshesHolderController MeshesHolder { get; private set; }

        public void Init(MeshesHolderController meshesHolder, ConstructionStructure structure)
        {
            Structure = structure;
            SetCollider(Structure.Size);

            Destroy(MeshesHolder?.gameObject);
            MeshesHolder = Instantiate(meshesHolder, transform.position, Quaternion.identity, transform);
        }

        public void Rotate(float scrollInput)
        {
            MeshesHolder.transform.Rotate(Vector3.up * (Mathf.Sign(scrollInput) * 45), Space.Self);
        }

        void Update()
        {
            // var overlapSize = Vector3.zero;//Structure.Size - (Vector3.forward * 0.3f) - (Vector3.right * 0.3f);
            // var collisions = Physics.OverlapBox(transform.position, overlapSize);

            // if (collisions.Length > 1)
            //     _renderer.material.color = _blockedColor;
            // else
            //     _renderer.material.color = _defaultColor;
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