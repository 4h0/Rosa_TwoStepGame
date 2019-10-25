using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public LayerMask cameraObstacle;
    public Vector3 offsetHeight;

    private PlayerController playerReference;
    private SphereCollider cameraTriggerCollider;
    private GameObject invisibleGameObject;                                  

    private void Awake()
    {
        playerReference = FindObjectOfType<PlayerController>();
        cameraTriggerCollider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        Detection();
    }

    private void LateUpdate()
    {
        if (Vector3.Distance(this.transform.position, (playerReference.transform.position + offsetHeight)) > 9)
        {
            FollowPlayer();
        }

        transform.LookAt(playerReference.transform.position + offsetHeight);
    }

    private void Detection()
    {
        Vector3 castDirection = playerReference.transform.position - transform.position;
        float rayLength = Vector3.Distance(transform.position, playerReference.transform.position);
        Ray cameraRay = new Ray(transform.position, castDirection * rayLength);
        RaycastHit cameraRayHit;

        bool rayHit = Physics.Raycast (cameraRay, out cameraRayHit, rayLength, cameraObstacle);
        
        if (rayHit)
        {
            GameObject hitGameObject = cameraRayHit.transform.gameObject;

            Debug.DrawLine(cameraRay.origin, cameraRayHit.point, Color.red, 3f);
            Debug.Log(hitGameObject.name);

            if (invisibleGameObject != null)
            {
                if (hitGameObject != invisibleGameObject)
                {
                    invisibleGameObject.GetComponent<MeshRenderer>().enabled = true;
                    hitGameObject.GetComponent<MeshRenderer>().enabled = false;
                    invisibleGameObject = hitGameObject;
                }
            }
            else
            {  
                hitGameObject.GetComponent<MeshRenderer>().enabled = false;
                invisibleGameObject = hitGameObject;
            }
        }
        else
        {
            Debug.DrawLine(cameraRay.origin, cameraRay.origin + cameraRay.direction * rayLength, Color.green);

            if (invisibleGameObject != null)
            {
                invisibleGameObject.GetComponent<MeshRenderer>().enabled = true;
                invisibleGameObject = null;
            }
        }

        StartCoroutine(RecastDetection());
    }

    private void FollowPlayer()
    {   
        /*
        if(playerReference.onGround)
        {
            offsetHeight = new Vector3(0, 1.5f, 0);
        }
        else
        {    
            offsetHeight = new Vector3(0, 3f * playerReference.jumpCounter, 0);
        } 
        */

        transform.position = Vector3.Lerp(this.transform.position, playerReference.cameraPosition.transform.position + offsetHeight, .03f);  

        /*
        transform.position = Vector3.Lerp(this.transform.position, moveToPosition.transform.position, 1f);
        transform.rotation = moveToPosition.transform.rotation;

        moveToRotation.SetFromToRotation(this.transform.position, moveToPosition.transform.position);
        transform.rotation = moveToRotation;
        */
    }

    IEnumerator RecastDetection()
    {
        yield return new WaitForSeconds(.15f);

        Detection();
    }
}
