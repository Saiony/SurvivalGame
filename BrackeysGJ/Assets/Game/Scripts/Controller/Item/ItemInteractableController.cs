namespace Game.Scripts.Controller.Item
{
    public class ItemInteractableController : Scripts.Controller.Item.Item
    {
        public void Further()
        {
            if (Fowardable)
            {
                var newGO = FowardTime();
                Shrink();
                Instantiate(newGO, this.transform);
                var component = newGO.GetComponent<ItemInteractableController>();
                component.Shrink(false);
                component.Expand();
                Destroy(gameObject);
            }
        }

        private void Shrink(bool animate=true)
        {
            // bool animate diz se o Shrink vai ter um tempo pra diminuir
            // Todo: @Mike
        }

        private void Expand(bool animate=true)
        {
            // Todo: @Mike
        }
    }
}