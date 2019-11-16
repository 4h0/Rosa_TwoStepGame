using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1TriggerScript_Khoa : MonoBehaviour
{
    public GameObject puzzle1Reference;

    private bool puzzle1DoOnce;

    private void Awake()
    {
        puzzle1DoOnce = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp" && !puzzle1DoOnce)
        {
            this.GetComponent<MeshRenderer>().material.color = Color.blue;
            puzzle1DoOnce = true;
            puzzle1Reference.GetComponent<Puzzle1_Khoa>().ChangeDirection();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PickUp" && puzzle1DoOnce)
        {
            this.GetComponent<MeshRenderer>().material.color = Color.red;
            puzzle1DoOnce = false;
            puzzle1Reference.GetComponent<Puzzle1_Khoa>().ChangeDirection();
        }
    }
}
