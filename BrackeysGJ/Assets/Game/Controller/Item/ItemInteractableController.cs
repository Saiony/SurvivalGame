using System;
using System.Diagnostics;

namespace Game.Controller.Item
{
    public class ItemInteractableController : Item
    {
        public void Further()
        {
            var newGO = FowardTime();
            if (newGO == this.gameObject)
                throw new ArgumentOutOfRangeException("Object is not fowardable");
            Shrink();
            Instantiate(newGO, this.transform);
            var component = newGO.GetComponent<ItemInteractableController>();
            component.InteractableItem.Passado = gameObject;
            component.Expand();
            Destroy(gameObject);
        }

        private void Shrink()
        {
            // Todo: @Mike
        }

        private void Expand()
        {
            // Todo: @Mike
        }
    }
}