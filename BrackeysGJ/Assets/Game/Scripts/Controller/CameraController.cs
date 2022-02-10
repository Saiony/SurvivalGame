using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Controller.Player;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject ThirdPersonCamera;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DisableThirdPersonCamera()
    {

    }

    public void EnableThirdPersonCamera()
    {

    }
}
