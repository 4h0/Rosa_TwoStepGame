using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ThirdPersonController : MonoBehaviour
{
    // Input deadzone
    public float inputDelay = 0.1f;
    public float playerSpd;

    public float forwardVelocity = 12f;
    public float rotateVelocity = 100f;

    // Holding rotation
    Quaternion targetRotation;

    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");

        Vector3 playerMovement = new Vector3(xMove, 0f, yMove).normalized * playerSpd * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
    }
}
   
