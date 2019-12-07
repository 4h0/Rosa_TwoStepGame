using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;

public class ThirdPersonCamera : MonoBehaviour
{
    public bool lockCursor;
    public Slider sensitivitySlider;
    public float mouseSensitivity;
    public Transform target;
    public float dstFromTarget = 2;
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    public float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;
    float yaw;
    float pitch;

    private PlayerController_Alex playerReference;

    private void Awake()
    {
        playerReference = FindObjectOfType<PlayerController_Alex>();
    }

    void Update()
    {
        CursorLock();
    }
    void LateUpdate()
    {
        if (playerReference.canMove)
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

            // Vector3 targetRotation = new Vector3 (pitch, yaw);
            transform.eulerAngles = currentRotation;

            transform.position = target.position - transform.forward * dstFromTarget;
        }
    }

    void CursorLock()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void SetSensitivity()
    {
        mouseSensitivity = sensitivitySlider.value;
    }
}
