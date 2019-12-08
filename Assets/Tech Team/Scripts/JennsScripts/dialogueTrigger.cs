using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus; // must be used. This allows the script to access the fungus scripts.
using UnityTemplateProjects; // this is for the camera. Has to be included for the scripts to talk.

public class dialogueTrigger : MonoBehaviour
{
    /*
    private Quest1_Khoa Quest1Reference; // referencing Khoa's script
    private Quest2_Khoa Quest2Reference; // referencing Khoa's script
    private Quest3_Khoa Quest3Reference; // referencing Khoa's script
    */
    private PlayerController_Alex CanMoveReference; // Referencing Khoa/Alex Script

    public Flowchart flowchart; // calls the flowchart.
    public bool taskDone;
    private bool hasAcceptedQuest1, hasAcceptedQuest2, hasAcceptedQuest3; // has the player talked to the npc
    private bool hasPlayer; // is the player in a collider? yes or no
    private bool isTalking; // is the npc talking
    private bool Quest1Pass, Quest2Pass, Quest3Pass;

    // public ThirdPersonCamera thirdPersonCamera; // For the inspector and reference. Drag and drop.

    private bool hasTalked; // is the player in a collider? yes or no

    private void Awake()
    {
        hasTalked = false;
        CanMoveReference = FindObjectOfType<PlayerController_Alex>();
    }

    private void Update()
    {
        Random_Dialogue();
        Quest1_Dialogue();
        Quest2_Dialogue();
        Quest3_Dialogue();

    }
    void Random_Dialogue()
    {
        if (hasPlayer && Input.GetKeyDown("k")) //is hasPlayer true or false? if it's true and key pressed then
        {
            if (this.gameObject.tag == "NPC") // checks to see if the radius is tagged with the NPC tag.
            {
                Debug.Log("NPC1");
                // thirdPersonCamera.enabled = false;
                flowchart.ExecuteBlock("Quest Dialogue"); // we execute the named block within the flowchart.

            }
            else if (this.gameObject.tag == "NPC2") // checks tag.
            {
                Debug.Log("Npc2");
                // thirdPersonCamera.enabled = false;
                flowchart.ExecuteBlock("Testing1"); // we execute the named block within the flowchart.
            }
            else if (this.gameObject.tag == "NPC3")
            {
                Debug.Log("Npc3");
                // thirdPersonCamera.enabled = false;
                flowchart.ExecuteBlock("FlavorHW"); // we execute the named block within the flowchart.
            }
            else if (this.gameObject.tag == "NPC4")
            {
                Debug.Log("Npc4");
                // thirdPersonCamera.enabled = false;
                flowchart.ExecuteBlock("FlavorC"); // we execute the named block within the flowchart.
            }
            else if (this.gameObject.tag == "NPC5")
            {
                Debug.Log("Npc5");
                // thirdPersonCamera.enabled = false;
                flowchart.ExecuteBlock("FlavorG"); // we execute the named block within the flowchart.
            }
        }
    }
    void Quest1_Dialogue()
    {
        if (hasPlayer && Input.GetKeyDown("k")) //is hasPlayer true or false? if it's true and key pressed then
        {
            if (!hasTalked)
            {
                if (this.gameObject.tag == "NPC6")
                {
                    Debug.Log("NPC6");
                    // thirdPersonCamera.enabled = false;
                    flowchart.ExecuteBlock("Quest1"); // executing the fire quest chain.
                    hasTalked = true;
                }
            }
            else if (!taskDone && hasTalked) // QUEST IN PROGRESS CHECKER!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            {
                if (this.gameObject.tag == "NPC6") //checks npc tag
                {
                    Debug.Log("Quest not done yet.");
                    // thirdPersonCamera.enabled = false;
                    flowchart.ExecuteBlock("IPfire"); // you know what this does by now :D
                }
            }
            else if (taskDone && hasTalked) // checks if task is COMPLETED!!!!!!
            {
                if (this.gameObject.tag == "NPC6")
                {
                    Debug.Log("Quest complete.");
                    // thirdPersonCamera.enabled = false;
                    flowchart.ExecuteBlock("fFire");
                }
            }
        }
    }
    void Quest3_Dialogue()
    {
        if (hasPlayer && Input.GetKeyDown("k")) //is hasPlayer true or false? if it's true and key pressed then
        {
            if (!hasTalked)
            {
                if (this.gameObject.tag == "NPC8")
                {
                    Debug.Log("NPC8");
                    // thirdPersonCamera.enabled = false;
                    flowchart.ExecuteBlock("Quest3");
                    hasTalked = true;
                }
                else if (this.gameObject.tag == "NPC9")
                {
                    Debug.Log("NPC9");
                    // thirdPersonCamera.enabled = false;
                    flowchart.ExecuteBlock("preFetch");
                }
            }
            else if (!taskDone && hasTalked) // QUEST IN PROGRESS CHECKER!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            {
                if (this.gameObject.tag == "NPC8")
                {
                    Debug.Log("In progress NPC8");
                    // thirdPersonCamera.enabled = false;
                    flowchart.ExecuteBlock("IPfetch");
                }
                else if (this.gameObject.tag == "NPC9")
                {
                    hasTalked = true;
                    Debug.Log("In progress NPC9");
                    // thirdPersonCamera.enabled = false;
                    flowchart.ExecuteBlock("fetchSecondary");
                    // Quest2Script.Delivered();

                }
            }
            else if (taskDone && hasTalked) // checks if task is COMPLETED!!!!!!
            {
                if (this.gameObject.tag == "NPC8")
                {
                    Debug.Log("Quest NPC8 is done.");
                    // thirdPersonCamera.enabled = false;
                    flowchart.ExecuteBlock("fFetch");
                }
                else if (this.gameObject.tag == "NPC9")
                {
                    Debug.Log("Quest NPC9 is done.");
                    // thirdPersonCamera.enabled = false;
                    flowchart.ExecuteBlock("fetchSecondary");
                }
            }
        }
    }
    void Quest2_Dialogue()
    {
        if (hasPlayer && Input.GetKeyDown("k")) //is hasPlayer true or false? if it's true and key pressed then
        {
            if (!hasTalked)
            {
                if (this.gameObject.tag == "NPC7")
                {
                    Debug.Log("NPC7");
                    // thirdPersonCamera.enabled = false;
                    flowchart.ExecuteBlock("Quest2");
                    hasTalked = true;
                }
            }
            else if (!taskDone && hasTalked) // QUEST IN PROGRESS CHECKER!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            {
                if (this.gameObject.tag == "NPC7" || this.gameObject.tag == "NPC10")
                {
                    Debug.Log("Strength Quest not done yet.");
                    // thirdPersonCamera.enabled = false;
                    flowchart.ExecuteBlock("IPstrength");
                }
            }
            else if (taskDone && hasTalked) // checks if task is COMPLETED!!!!!!
            {
                if (this.gameObject.tag == "NPC10")
                {
                    Debug.Log("Strength Quest Complete");
                    // thirdPersonCamera.enabled = false;
                    flowchart.ExecuteBlock("fStrength");
                }
            }
        }
    }

    public void FreezePlayer()
    {
        CanMoveReference.canMove = false;
    }
    public void UnfreezePlayer()
    {
        CanMoveReference.canMove = true;
        Debug.Log("this worked");
    }

    void OnTriggerEnter(Collider other) // collider stuff
    {

        // Debug.Log("Entered"); // testing to see if entered
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
