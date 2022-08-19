using Game.Scripts.Controller.Crafting;
using Game.Scripts.Controller.Crafting.Construction;
using Game.Scripts.Domain.Crafting;
using Game.Scripts.Domain.Items;
using UnityEngine;

namespace BrackeysGJ.Assets.Game.Scripts.Controller.Crafting.Construction
{
    public class ConstructionController : MonoBehaviour
    {        
        [SerializeField]
        private ConstructionPlaceholderController StructPlaceholderPrefab;
        
        private ConstructionPlaceholderController _constructionPlaceholder;
        private RaycastHit _raycastHit;
        private Transform _camChild;
        private CraftingRecipe _recipe;
        
        //debug
        public GameObject raycastHit;

        public void Init(Transform camChildTransform)
        {
            _camChild = camChildTransform;
            _constructionPlaceholder = Instantiate(StructPlaceholderPrefab, transform);
        }

        public void SetRecipe(CraftingRecipe recipe)
        {
            _recipe = recipe;
            
            var meshesHolder = _recipe.Item.Prefab.GetComponent<BuildingController>().MeshesHolder;
            _constructionPlaceholder.Init(meshesHolder, (ConstructionStructure)recipe.Item);
        }

        private void Update()
        {
            if (_constructionPlaceholder == null)
                return;

            Debug.DrawRay(_camChild.position + (_camChild.forward * 3), _camChild.forward, Color.green);
            if (Physics.Raycast(_camChild.position + (_camChild.forward * 3), _camChild.forward, out _raycastHit, 10f))
            {
                raycastHit = _raycastHit.collider.gameObject;
                var finalPos = _raycastHit.point;
                finalPos = new Vector3(
                                        Mathf.Round(finalPos.x),
                                        Mathf.Round(finalPos.y),
                                        Mathf.Round(finalPos.z)
                                      );

                //offsets position closer to player
                var diffX = _camChild.position.x - finalPos.x;
                var diffZ = _camChild.position.z - finalPos.z;
                if (Mathf.Abs(diffX) > Mathf.Abs(diffZ)) //modifica valor em x
                {
                    var dirX = diffX < 0 ? -1 : 1;
                    var offset = (_constructionPlaceholder.Structure.Size.z * ((float)dirX / 2));
                    finalPos.x += (int)offset;
                }
                else //modifica valor em z
                {
                    var dirZ = diffZ < 0 ? -1 : 1;
                    var offset = (_constructionPlaceholder.Structure.Size.z * ((float)dirZ / 2));
                    finalPos.z += (int)offset;
                }

                _constructionPlaceholder.transform.position = finalPos;
            }

            if (Input.GetKeyDown(KeyCode.F))
                Instantiate(_recipe.Item.Prefab, _constructionPlaceholder.transform.position, _constructionPlaceholder.MeshesHolder.transform.rotation);

            var scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput != 0)
                _constructionPlaceholder.Rotate(scrollInput);
        }
    }
}