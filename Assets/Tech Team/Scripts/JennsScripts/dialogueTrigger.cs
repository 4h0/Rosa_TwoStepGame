using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus; // must be used. This allows the script to access the fungus scripts.
using UnityTemplateProjects; // this is for the camera. Has to be included for the scripts to talk.

public class dialogueTrigger : MonoBehaviour
{
    private Quest1_Khoa Quest1Reference; // referencing Khoa's script
    private Quest2_Khoa Quest2Reference; // referencing Khoa's script
    private Quest3_Khoa Quest3Reference; // referencing Khoa's script

    public Flowchart flowchart; // calls the flowchart.
    public ThirdPersonCamera thirdPersonCamera; // For the inspector and reference. Drag and drop.

    public bool taskDone;
    private bool hasAcceptedQuest1, hasAcceptedQuest2, hasAcceptedQuest3; // has the player talked to the npc
    private bool hasPlayer; // is the player in a collider? yes or no

    private bool Quest1Pass, Quest2Pass, Quest3Pass;

    void Awake()
    {
        Quest1Reference = FindObjectOfType<Quest1_Khoa>();
        Quest2Reference = FindObjectOfType<Quest2_Khoa>();
        Quest3Reference = FindObjectOfType<Quest3_Khoa>();
        hasAcceptedQuest1 = false;
        hasAcceptedQuest2 = false;
        hasAcceptedQuest3 = false;
        taskDone = false;
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
                thirdPersonCamera.enabled = false;
                flowchart.ExecuteBlock("Quest Dialogue"); // we execute the named block within the flowchart.
       
            }
            else if (this.gameObject.tag == "NPC2") // checks tag.
            {
                Debug.Log("Npc2");
                thirdPersonCamera.enabled = false;
                flowchart.ExecuteBlock("Testing1"); // we execute the named block within the flowchart.
            }
            else if (this.gameObject.tag == "NPC3")
            {
                Debug.Log("Npc3");
                thirdPersonCamera.enabled = false;
                flowchart.ExecuteBlock("FlavorHW"); // we execute the named block within the flowchart.
            }
            else if (this.gameObject.tag == "NPC4")
            {
                Debug.Log("Npc4");
                thirdPersonCamera.enabled = false;
                flowchart.ExecuteBlock("FlavorC"); // we execute the named block within the flowchart.
            }
            else if (this.gameObject.tag == "NPC5")
            {
                Debug.Log("Npc5");
                thirdPersonCamera.enabled = false;
                flowchart.ExecuteBlock("FlavorG"); // we execute the named block within the flowchart.
            }
        }
    }
    void Quest1_Dialogue()
    {
        hasAcceptedQuest1 = flowchart.GetBooleanVariable("quest1Accepted");
        Quest1Pass = Quest1Reference.quest1Pass;

        if (hasPlayer && Input.GetKeyDown("k")) //is hasPlayer true or false? if it's true and key pressed then
        {
            if ((!hasAcceptedQuest1 && !Quest1Pass))
            {
                if (this.gameObject.tag == "NPC6")
                {
                    Debug.Log("NPC6");
                    thirdPersonCamera.enabled = false;
                    flowchart.ExecuteBlock("Quest1"); // executing the fire quest chain.
                    // hasAcceptedQuest1 = true;
                }
            }
            else if (hasAcceptedQuest1 && !Quest1Pass) // QUEST IN PROGRESS CHECKER!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            {
                if (this.gameObject.tag == "NPC6") //checks npc tag
                {
                    Debug.Log("Quest not done yet.");
                    thirdPersonCamera.enabled = false;
                    flowchart.ExecuteBlock("IPfire"); // you know what this does by now :D
                }
            }
            else if (Quest1Pass) // checks if task is COMPLETED!!!!!!
            {
                if (this.gameObject.tag == "NPC6")
                {
                    Debug.Log("Quest complete.");
                    thirdPersonCamera.enabled = false;
                    flowchart.ExecuteBlock("fFire");
                }
            }
        }
    }
    void Quest3_Dialogue()
    {
        hasAcceptedQuest3 = flowchart.GetBooleanVariable("quest3Accepted");
        Quest3Pass = Quest3Reference.quest3Pass;

        if (hasPlayer && Input.GetKeyDown("k")) //is hasPlayer true or false? if it's true and key pressed then
        {
            if (!hasAcceptedQuest3)
            {
                if (this.gameObject.tag == "NPC8")
                {
                    Debug.Log("NPC8");
                    thirdPersonCamera.enabled = false;
                    flowchart.ExecuteBlock("Quest3");
                    hasAcceptedQuest3 = true;
                }
                else if (this.gameObject.tag == "NPC9")
                {
                    Debug.Log("NPC9");
                    thirdPersonCamera.enabled = false;
                    flowchart.ExecuteBlock("preFetch");
                }
            }
            else if (!Quest3Pass && hasAcceptedQuest3) // QUEST IN PROGRESS CHECKER!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            {
                if (this.gameObject.tag == "NPC8")
                {
                    Debug.Log("In progress NPC8");
                    thirdPersonCamera.enabled = false;
                    flowchart.ExecuteBlock("IPfetch");
                }
                else if (this.gameObject.tag == "NPC9")
                {
                    hasAcceptedQuest3 = true;
                    Debug.Log("In progress NPC9");
                    thirdPersonCamera.enabled = false;
                    flowchart.ExecuteBlock("fetchSecondary");
                    // Quest2Script.Delivered();
                    
                }
            }
            else if (Quest3Pass) // checks if task is COMPLETED!!!!!!
            {
                if (this.gameObject.tag == "NPC8")
                {
                    Debug.Log("Quest NPC8 is done.");
                    thirdPersonCamera.enabled = false;
                    flowchart.ExecuteBlock("fFetch");
                }
                else if (this.gameObject.tag == "NPC9")
                {
                    Debug.Log("Quest NPC9 is done.");
                    thirdPersonCamera.enabled = false;
                    flowchart.ExecuteBlock("fetchSecondary");
                }
            }
        }
    }
    void Quest2_Dialogue()
    {
        hasAcceptedQuest2 = flowchart.GetBooleanVariable("quest2Accepted");
        Quest2Pass = Quest2Reference.quest2Pass;

        if (hasPlayer && Input.GetKeyDown("k")) //is hasPlayer true or false? if it's true and key pressed then
        {    
            if (!hasAcceptedQuest2)
            {
                if (this.gameObject.tag == "NPC7")
                {
                    Debug.Log("NPC7");
                    thirdPersonCamera.enabled = false;
                    flowchart.ExecuteBlock("Quest2");
                    // hasTalkedQuest2 = true;
                }
            }
            else if (!Quest2Pass && hasAcceptedQuest2) // QUEST IN PROGRESS CHECKER!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            {
                if (this.gameObject.tag == "NPC7" || this.gameObject.tag == "NPC10")
                {
                    Debug.Log("Strength Quest not done yet.");
                    thirdPersonCamera.enabled = false;
                    flowchart.ExecuteBlock("IPstrength");
                }
            }
            else if (Quest2Pass) // checks if task is COMPLETED!!!!!!
            {
                if (this.gameObject.tag == "NPC10")
                {
                    Debug.Log("Strength Quest Complete");
                    thirdPersonCamera.enabled = false;
                    flowchart.ExecuteBlock("fStrength");
                }
            }
        }
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
