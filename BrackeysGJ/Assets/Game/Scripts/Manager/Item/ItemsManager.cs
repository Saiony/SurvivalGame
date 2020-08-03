using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Game.ScriptableObjects;
public class ItemsManager : MonoBehaviour
{
    [SerializeField]
    private List<InteractableItemSO>  interactableItemsSO;

    public static ItemsManager Instance = null;

    void Awake()
    {
        if(!Instance)
            throw new Exception("Singleton already populated");
        Instance = this;
    }

    public InteractableItemSO GetFutureOf()
    {
        
    }

    public InteractableItemSO 
}
