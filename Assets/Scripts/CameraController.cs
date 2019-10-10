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
        transform.LookAt(playerReference.transform.position + offsetHeight);

        if (Vector3.Distance(this.transform.position, (playerReference.transform.position + offsetHeight)) > 9)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {  
        transform.position = Vector3.Lerp(this.transform.position, playerReference.cameraPosition.transform.position, .03f);  

        /*
        transform.position = Vector3.Lerp(this.transform.position, moveToPosition.transform.position, 1f);
        transform.rotation = moveToPosition.transform.rotation;

        moveToRotation.SetFromToRotation(this.transform.position, moveToPosition.transform.position);
        transform.rotation = moveToRotation;
        */
    }
}
