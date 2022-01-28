using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Controller.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform FollowTransform;

    private Vector2 Look;

    public void OnLook(InputValue value)
    {
        _look = value.Get<Vector2>();
    }

    private void Update()
    {
        //Rotate the Follow Target transform based on the input
        FollowTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);
        FollowTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * rotationPower, -Vector3.right);

        var angles = FollowTransform.transform.localEulerAngles;
        angles.z = 0;

        var angle = FollowTransform.transform.localEulerAngles.x;

        //Clamp rotation
        if (angle > 180 && angle < 340)
            angles.x = 340;
        else if(angle < 180 && angle > 40)
            angles.x = 40;

        FollowTransform.transform.localEulerAngles = angles;
    }
}
