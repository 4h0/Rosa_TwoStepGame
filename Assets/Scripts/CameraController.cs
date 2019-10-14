using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offsetHeight;

    private PlayerController playerReference;
    private SphereCollider cameraTriggerCollider;
    private GameObject savedGameObject;
                                  
    private int cameraObstacle;

    private void Awake()
    {
        playerReference = FindObjectOfType<PlayerController>();
        cameraTriggerCollider = GetComponent<SphereCollider>();

        cameraObstacle = LayerMask.GetMask("cameraObstacle");
    }

    private void Start()
    {
        Detection();
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
    private void Detection()
    {
        RaycastHit cameraRayHit;
        Debug.DrawLine(this.transform.position, new Vector3(0,1,0) + playerReference.transform.position, Color.red, 30f);
        bool rayHit = Physics.Raycast(this.transform.position, new Vector3(0, 1, 0) + playerReference.transform.position, out cameraRayHit, Mathf.Infinity, cameraObstacle);

        if (rayHit)
        {
            GameObject hitGameObject = cameraRayHit.transform.gameObject;
            Debug.Log("target: " + hitGameObject.name);

            savedGameObject = hitGameObject;
            hitGameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            if (savedGameObject != null)
            {
                savedGameObject.GetComponent<MeshRenderer>().enabled = true;
                savedGameObject = null;
            }
        }

        StartCoroutine(RayCastingWait());

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

    IEnumerator RayCastingWait()
    {
        yield return new WaitForSeconds(.15f);

        Detection();
    }   
}
