using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1_Khoa : MonoBehaviour
{
    public Transform startPoint, endPoint;

    private Transform moveToDestination;

    private int turnBack;

    private void Awake()
    {
        moveToDestination = new GameObject().transform;
        moveToDestination.position = startPoint.position;

        turnBack = 0;
        StartCoroutine(MoveDoor());
    }

    IEnumerator MoveDoor()
    {
        this.transform.position = Vector3.Lerp(transform.position, moveToDestination.position, .03f);

        yield return new WaitForEndOfFrame();

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
