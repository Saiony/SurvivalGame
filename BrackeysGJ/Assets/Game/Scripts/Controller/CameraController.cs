using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Controller.Player;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private PlayerController _player = null;
    private PlayerController Player => _player;

    [SerializeField]
    private float _offsetX = 0;
    private float OffsetX => _offsetX;

    [SerializeField]
    private float _offsetY = 0;
    private float OffsetY => _offsetY;

    [SerializeField]
    private float _offsetZ = 0;
    private float OffsetZ => _offsetZ;

    private void FixedUpdate()
    {
        transform.position = new Vector3
        (
            Player.transform.position.x + OffsetX,
            Player.transform.position.y + OffsetY,
            Player.transform.position.z + OffsetZ
        );
    }
}
