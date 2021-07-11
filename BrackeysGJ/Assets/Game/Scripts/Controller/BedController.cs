using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Controller.Player;
using Game.Scripts.Controller.Time;
using UnityEngine;

public class BedController : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Collider _detectionCollider = null;
    public Collider DetectionCollider => _detectionCollider;

    public void OnInteract()
    {
        TimeController.Instance.PassDay(6);
    }
}
