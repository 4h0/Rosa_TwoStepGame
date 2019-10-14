using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthLogic : MonoBehaviour
{
    private PlayerController playerReference;
    private Rigidbody pickUpRigidbody;
    private MeshRenderer changeCubeColor;

    public float timerBeforeDestroy;

    private void Awake()
    {
        playerReference = FindObjectOfType<PlayerController>();
        pickUpRigidbody = GetComponent<Rigidbody>();
        changeCubeColor = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        pickUpRigidbody.useGravity = false;
        pickUpRigidbody.isKinematic = true;
        changeCubeColor.material.color = Color.white;
    }

    private void Update()
    {
        if(timerBeforeDestroy >= 0)
        {
            timerBeforeDestroy -= Time.deltaTime;
            transform.position = Vector3.Lerp(this.transform.position, playerReference.strengthPosition.transform.position, .06f);   
        }
        else
        {
            changeCubeColor.material.color = Color.green;

            pickUpRigidbody.useGravity = true;
            pickUpRigidbody.isKinematic = false;

            Destroy(this);
        }
    }
}
