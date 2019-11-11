using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1_Khoa : MonoBehaviour
{
    public Transform destination;

    private Rigidbody doorRigidbody;
    private Transform originalPosition;
    private Transform movetoDestination;

    private bool canChange;
    private int turnBack;
    private int multiplier;

    private void Awake()
    {
        doorRigidbody = GetComponent<Rigidbody>();

        originalPosition = this.transform;
        movetoDestination = originalPosition;

        turnBack = 0;
    }

    private void FixedUpdate()
    {
        if(canChange)
        {
            doorRigidbody.AddForce(transform.right * multiplier * Time.deltaTime * 300, ForceMode.Acceleration); 
        }
        else
        {
            doorRigidbody.velocity = new Vector3(0, 0, 0);
        }
    }

    IEnumerator CanMove()
    {
        canChange = true;

        yield return new WaitForSeconds(3f);

        canChange = false;
    }

    public void ChangeDirection()
    {
        turnBack++;
        StartCoroutine(CanMove());

        if (turnBack % 2 == 0)
        {
            multiplier = -1;
        }
        else
        {
            multiplier = 1;
        }
    }
}
