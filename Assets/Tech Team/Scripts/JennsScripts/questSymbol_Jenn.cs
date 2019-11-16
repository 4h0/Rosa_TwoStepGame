using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questSymbol_Jenn : MonoBehaviour
{

    private bool hasPlayer; // is the player in a collider? yes or no
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown("k") && hasPlayer)
        {
            rend.enabled = false;
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

    void OnTriggerExit(Collider other) //when leaving the trigger
    {
        if (other.CompareTag("Player")) // checks for the player tag
        {
            hasPlayer = false; // sets hasPlayer to be false so dialogue won't play.
            rend.enabled = false;
         }
    }
}
