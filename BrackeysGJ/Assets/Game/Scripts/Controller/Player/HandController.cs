
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField]
    private Collider _contactArea = null;
    private Collider ContactArea => _contactArea;

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
        if (damageable == null)
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
}

public interface IDetectionListener
{
    void OnDetect(IDamageable interactable);
}