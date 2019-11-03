using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthLogic_Khoa : MonoBehaviour
{
    private PlayerController_Khoa playerReference;
    private Rigidbody pickUpRigidbody;
    private MeshRenderer changeCubeColor;

    public float timerBeforeDestroy;

    private void Awake()
    {
        playerReference = FindObjectOfType<PlayerController_Khoa>();
        pickUpRigidbody = GetComponent<Rigidbody>();
        changeCubeColor = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        pickUpRigidbody.useGravity = false;
        changeCubeColor.material.color = Color.white;
    }

    private void Update()
    {
        if(timerBeforeDestroy > 0)
        {
            timerBeforeDestroy -= Time.deltaTime;
            transform.position = Vector3.Lerp(this.transform.position, playerReference.strengthPosition.transform.position, .06f);   
        }
        else
        {
            playerReference.strengthEnd = true;

            changeCubeColor.material.color = Color.green;
            pickUpRigidbody.useGravity = true;

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
