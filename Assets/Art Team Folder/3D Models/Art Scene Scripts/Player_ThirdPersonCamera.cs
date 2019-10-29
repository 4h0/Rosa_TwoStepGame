using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ThirdPersonCamera : MonoBehaviour
{
    public float rotateSpd = 1;
    public Transform Target, Player;

    float mouseX, mouseY;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    void LateUpdate()
    {
        CameraControl();
    }

    void CameraControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotateSpd;
        mouseY -= Input.GetAxis("Mouse Y") * rotateSpd;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(Target);

        Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        Player.rotation = Quaternion.Euler(0, mouseX, 0);

    }
}
