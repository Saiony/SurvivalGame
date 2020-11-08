using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Controller.Interact;
using Game.Scripts.Controller.Time;
using UnityEngine;

public class BedController : Interactable
{
    protected override void OnPlayerEnter()
    {
    }

    protected override void OnPlayerExit()
    {
    }

    protected override void OnPlayerInteract()
    {
        TimeController.Instance.PassDay(6);
    }
}
