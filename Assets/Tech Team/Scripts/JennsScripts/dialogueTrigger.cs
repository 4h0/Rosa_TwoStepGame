using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus; // must be used. This allows the script to access the fungus scripts.

public class dialogueTrigger : MonoBehaviour
{
    public Flowchart flowchart; // grants access/calls the flowchart I'm putting the dialogue boxes/info in.
    public GameObject NPC;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && triggerStay)
        {
            //
        }
    }

    bool triggerStay = false;

    void OnTriggerStay(Collider collision) 
        {
            if (collision.gameObject.CompareTag("NPC")) // checking to see if the GameObject is tagged as an NPC && the Q key is being pressed.
            {
                triggerStay = true;
                flowchart.ExecuteBlock("NPC Dialogue"); // executes the NPC Dialogue box within the flowchart.
                Debug.Log("Collision & Button press detected."); //Debug to check for collision.
                 
                
            }

            else
            {
                Debug.Log("Dialogue not present."); // Debug to see if there's no collision.
            }
        }

    void OnTriggerExit(Collider collision)
    {
        Debug.Log("Exited");
        if (collision.gameObject.CompareTag("NPC"))
        {
            triggerStay = false;
        }
    }
}
