using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController playerReference;
    private SphereCollider cameraTriggerCollider;

    public Vector3 offsetHeight;

    private void Awake()
    {
        playerReference = FindObjectOfType<PlayerController>();
        cameraTriggerCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (Vector3.Distance(this.transform.position, (playerReference.transform.position + offsetHeight)) > 9)
        {
            FollowPlayer();
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(playerReference.transform.position + offsetHeight);
    }

    private void FollowPlayer()
    {  
        transform.position = Vector3.Lerp(this.transform.position, playerReference.cameraPosition.position, .03f);  

        /*
        transform.position = Vector3.Lerp(this.transform.position, moveToPosition.transform.position, 1f);
        transform.rotation = moveToPosition.transform.rotation;

        moveToRotation.SetFromToRotation(this.transform.position, moveToPosition.transform.position);
        transform.rotation = moveToRotation;
        */
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
            cameraTriggerCollider.radius = 3f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            collision.gameObject.GetComponent<MeshRenderer>().enabled = true;
            cameraTriggerCollider.radius = 1f;
        }
    }
}
