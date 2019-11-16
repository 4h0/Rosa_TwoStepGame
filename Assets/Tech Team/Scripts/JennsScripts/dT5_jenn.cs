using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class dT5_jenn : MonoBehaviour
{
    public Flowchart flowchart; // calls the flowchart.
    private bool hasPlayer; // is the player in a collider? yes or no

    private void Update()
    {
        if (hasPlayer && Input.GetKeyDown("k")) //is hasPlayer true or false? if it's true and key pressed then
        {
            flowchart.ExecuteBlock("FlavorC"); // we execute the named block within the flowchart.
        }
    }


    void OnTriggerEnter(Collider other) // collider stuff
    {
        Debug.Log("Entered"); // testing to see if entered
        if (other.CompareTag("Player")) //if the player is colliding with trigger
        {
            hasPlayer = true; // set hasPlayer to true!
        }
    }

    private void OnTriggerExit(Collider other) //when leaving the trigger
    {
        if (other.CompareTag("Player")) // checks for the player tag
        {
            hasPlayer = false; // sets hasPlayer to be false so dialogue won't play.
        }
    }
}
