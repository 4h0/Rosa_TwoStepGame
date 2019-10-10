using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController playerReference;

    public Vector3 offsetHeight;

    private void Awake()
    {
        playerReference = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        transform.LookAt(playerReference.transform.position + offsetHeight);

        if(Vector3.Distance(this.transform.position, (playerReference.transform.position + offsetHeight)) > 4)
        {
            transform.position = Vector3.Lerp(this.transform.position, playerReference.cameraPosition.transform.position, .1f);
        }
        /*
        transform.position = Vector3.Lerp(this.transform.position, moveToPosition.transform.position, 1f);
        transform.rotation = moveToPosition.transform.rotation;

        moveToRotation.SetFromToRotation(this.transform.position, moveToPosition.transform.position);
        transform.rotation = moveToRotation;
        */
    }
}
