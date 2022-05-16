using Game.Scripts.Controller.Itens;
using Game.Scripts.Domain.Interface.Items;
using Game.Scripts.Domain.Items;
using UnityEngine;

public class ObjectPickerController : MonoBehaviour
{
    [SerializeField]
    private Collider _trigger = null;
    private Collider Trigger => _trigger;

    private IObjectPickerListener Listener { get; set; }

    public void Init(IObjectPickerListener listener)
    {
        Listener = listener;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Gatherable")
        {
            var item = col.gameObject.GetComponent<ItemController>();
            item.DestroyItself();
            Listener.OnObjectPicked(item.Item);
        }
    }
}

public interface IObjectPickerListener
{
    void OnObjectPicked(IItem item);
}
