using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject moveToPosition;

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        transform.position = Vector3.Lerp(this.transform.position, moveToPosition.transform.position, 1f);
        transform.rotation = moveToPosition.transform.rotation;

        /*
        moveToRotation.SetFromToRotation(this.transform.position, moveToPosition.transform.position);
        transform.rotation = moveToRotation;
        */
    }
}
