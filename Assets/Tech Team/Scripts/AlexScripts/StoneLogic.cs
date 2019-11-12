using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneLogic : MonoBehaviour
{
    private Rigidbody stoneRigidBody;
    private Puzzle1_Khoa puzzle1Reference;

    private bool puzzle1DoOnce;

    private void Awake()
    {       
        stoneRigidBody = GetComponent<Rigidbody>();
        puzzle1Reference = FindObjectOfType<Puzzle1_Khoa>();

        puzzle1DoOnce = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Puzzle1Trigger" && !puzzle1DoOnce)
        {
            puzzle1DoOnce = true;
            puzzle1Reference.ChangeDirection();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Puzzle1Trigger" && puzzle1DoOnce)
        {
            puzzle1DoOnce = false;
            puzzle1Reference.ChangeDirection();
        }
    }
}
