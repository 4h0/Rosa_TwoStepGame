using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthLogic_Khoa : MonoBehaviour
{
    private PlayerController_Alex playerReference;
    private Rigidbody pickUpRigidbody;
    private MeshRenderer changeMaterial;

    public float timerBeforeDestroy;

    private void Awake()
    {
        while (playerReference == null)
        {
            playerReference = FindObjectOfType<PlayerController_Alex>();
        }

        pickUpRigidbody = GetComponent<Rigidbody>();
        changeMaterial = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        changeMaterial.material = playerReference.savedMaterial[0];

        pickUpRigidbody.useGravity = false;
        pickUpRigidbody.freezeRotation = true;
    }

    private void Update()
    {
        if(timerBeforeDestroy > 0)
        {
            timerBeforeDestroy -= Time.deltaTime;
            transform.position = Vector3.Lerp(this.transform.position, playerReference.strengthRayEndPoint.transform.position, .06f);   
        }
        else
        {
            playerReference.strengthEnd = true;

            pickUpRigidbody.useGravity = true;
            pickUpRigidbody.freezeRotation = false;

            Destroy(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Rigidbody>() == null && collision.gameObject.tag != "Ground")
        {
            timerBeforeDestroy = 0;
        }
    }
}
