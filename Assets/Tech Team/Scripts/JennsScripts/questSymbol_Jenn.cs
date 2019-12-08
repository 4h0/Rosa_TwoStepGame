using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questSymbol_Jenn : MonoBehaviour
{

    private bool hasPlayer; // is the player in a collider? yes or no
    Renderer rend; // calls to the mesh renderer.
    // public GameObject npcRadius; // making a reference to the game object whose collision it'll be entering
    public GameObject QuestSymbol; 

    void Start()
    {
        
        // rend = GetComponent<Renderer>(); // gets the renderer and labels it true, so the item is rendered in the scene.
        rend = QuestSymbol.GetComponent<Renderer>();
        rend.enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown("k") && hasPlayer) // checks to see if the player has entered the npc's collider and 'k' is being pressed.
        {
            rend.enabled = false; // turns the quest indicator off.
        }
    }

    void OnTriggerEnter(Collider other) // collider stuff
    {
        // Debug.Log("Entered"); // testing to see if entered
        if (other.CompareTag("Player")) //&& (other.gameObject.name == "npcRadius")) //if the player is colliding with trigger
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
