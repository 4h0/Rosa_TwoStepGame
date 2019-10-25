using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashHelper : MonoBehaviour
{
    private PlayerController playerReference;

    public bool canStart;

    private void Awake()
    {
        playerReference = FindObjectOfType<PlayerController>();

        canStart = false;
    }

    public void StartChecking()
    {
        StartCoroutine(GetDirection());
    }

    IEnumerator GetDirection()
    {
        this.transform.position = playerReference.transform.position;
        playerReference.canMove = false;

        yield return new WaitForSeconds(.15f);

        if (transform.position == playerReference.transform.position)
        {
            transform.rotation = playerReference.transform.rotation;
        }
        else
        {
            transform.LookAt(playerReference.transform.position);
        }

        yield return new WaitForSeconds(.15f);

        canStart = true;
    }
}
