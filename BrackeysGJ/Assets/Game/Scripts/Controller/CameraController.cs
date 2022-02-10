using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Game.Scripts.Controller.Player;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private CinemachineFreeLook ThirdPersonCamera;

    public static CameraController Instance;

    private Vector2 InputMaxSpeed;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        InputMaxSpeed = new Vector2(ThirdPersonCamera.m_XAxis.m_MaxSpeed, ThirdPersonCamera.m_YAxis.m_MaxSpeed);
    }

    //TODO: escalar para um sistema de contexto
    public void OnInventoryEnter()
    {
        Cursor.lockState = CursorLockMode.None;
        
        ThirdPersonCamera.m_XAxis.m_MaxSpeed = 0f;
        ThirdPersonCamera.m_YAxis.m_MaxSpeed = 0f;
    }

    public void OnInventoryExit()
    {
        Cursor.lockState = CursorLockMode.Locked;

        ThirdPersonCamera.m_XAxis.m_MaxSpeed = InputMaxSpeed.x;
        ThirdPersonCamera.m_YAxis.m_MaxSpeed = InputMaxSpeed.y;
    }
}
