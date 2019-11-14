using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1_Khoa : MonoBehaviour
{
    public Transform parenting, endPoint;

    private Transform startPoint, moveToDestination;

    private int turnBack;

    private void Awake()
    {
        startPoint = new GameObject().transform;
        startPoint.SetParent(parenting, true);
        startPoint.position = this.transform.position;

        moveToDestination = new GameObject().transform;
        moveToDestination.SetParent(parenting, true);
        moveToDestination.position = startPoint.position;

        turnBack = 0;
        StartCoroutine(MoveDoor());
    }

    IEnumerator MoveDoor()
    {
        this.transform.position = Vector3.Lerp(transform.position, moveToDestination.position, .03f);

        yield return new WaitForSeconds(.06f);

        StartCoroutine(MoveDoor());
    }

    public void ChangeDirection()
    {
        turnBack++;

        if (turnBack % 2 == 0)
        {
            moveToDestination.position = startPoint.position;
        }
        else
        {
            moveToDestination.position = endPoint.position;
        }
    }
}
