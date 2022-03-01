
using System;
using System.Collections.Generic;
using System.Linq;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Items;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField]
    private Collider _contactArea = null;
    private Collider ContactArea => _contactArea;

    [SerializeField]
    private Transform _handPosition = null;
    private Transform HandPosition => _handPosition;
    private GameObject InstantiatedObject = null;

    private IDetectionListener Listener { get; set; }


    // private List<T> GetInteractablesOnRange<T>() where T : IBaseInteractable
    // {
    //     var results = Physics.OverlapBox(ContactArea.transform.position, ContactArea.bounds.size, Quaternion.identity);
    //     var interactableList = new List<T>();
    //     results.ToList().ForEach(x =>
    //     {
    //         var interactable = x.GetComponent<T>();
    //         if (interactable != null)
    //             interactableList.Add(interactable);
    //     });
    //     return interactableList;
    // }

    private void OnTriggerEnter(Collider col)
    {
        var damageable = col.gameObject.GetComponent<IDamageable>();
        if (damageable == null || col.tag == "Player")
            return;

        Listener?.OnDetect(damageable);
    }

    public void EnableDetection(IDetectionListener listener)
    {
        Listener = listener;
        ContactArea.enabled = true;
    }

    public void DisableDetection()
    {
        Listener = null;
        ContactArea.enabled = false;
    }

    public void EndAction()
    {
        ContactArea.enabled = false;
    }

    public void EquipItem(IEquipment equipment)
    {
        if(equipment == null)
        {
            UnequipItem();
            return;
        }
            
        InstantiatedObject = Instantiate(equipment.Prefab, HandPosition.position, HandPosition.rotation, transform);
    }

    public void UnequipItem()
    {
        if(InstantiatedObject == null)
            return;

        Destroy(InstantiatedObject);
        InstantiatedObject = null;
    }
}

public interface IDetectionListener
{
    void OnDetect(IDamageable interactable);
}