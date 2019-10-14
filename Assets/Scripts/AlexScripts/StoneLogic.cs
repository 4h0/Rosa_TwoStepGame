using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneLogic : MonoBehaviour
{
    private Rigidbody stoneRigidBody;

    private void Awake()
    {
        stoneRigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag =="Player")
        {
            stoneRigidBody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    private void OnCollisionExit(Collision collision)
    {   
        if(collision.gameObject.tag == "Player")
        {
            stoneRigidBody.constraints = RigidbodyConstraints.None;
        }
    }
}
