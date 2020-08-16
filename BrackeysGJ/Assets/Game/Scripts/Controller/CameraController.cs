using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Controller.Player;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private float OffsetX = 0;
    [SerializeField]
    private float OffsetY = 0;
    [SerializeField]
    private float offsetZ = 0;

    private void FixedUpdate() 
    {
        transform.position = new Vector3
        (
            player.transform.position.x + OffsetX, 
            player.transform.position.y + OffsetY, 
            player.transform.position.z + offsetZ
        );  
    }
}
