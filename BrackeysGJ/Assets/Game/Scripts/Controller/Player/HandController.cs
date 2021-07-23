
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField]
    private Collider _contactArea = null;
    private Collider ContactArea => _contactArea;

    private InventoryController Inventory { get; set; }
    private Item SelectedItem => Inventory.SelectedItem;


    public void Init(InventoryController inventory)
    {
        Inventory = inventory;
    }

    private List<T> GetInteractablesOnRange<T>() where T : IBaseInteractable
    {
        var results = Physics.OverlapBox(ContactArea.transform.position, ContactArea.bounds.size, Quaternion.identity);
        var interactableList = new List<T>();
        results.ToList().ForEach(x =>
        {
            var interactable = x.GetComponent<T>();
            if (interactable != null)
                interactableList.Add(interactable);
        });
        return interactableList;
    }

    public void EndAction()
    {
        ContactArea.enabled = false;
    }


}