using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus; // must be used. This allows the script to access the fungus scripts.

public class dialogueTrigger : MonoBehaviour
{
    public Flowchart flowchart; // calls the flowchart.
    private bool hasPlayer; // is the player in a collider? yes or no
    private PauseMenuController_Khoa pauseMenuReference;
    public int questType;
    public GameObject[] taskRelatedGameObject;
    private void Awake()
    {
        pauseMenuReference = FindObjectOfType<PauseMenuController_Khoa>();
        questType = flowchart.GetIntegerVariable("questType");
    }
    private void Update()
    {
        if (hasPlayer && Input.GetKeyDown("k")) //is hasPlayer true or false? if it's true and key pressed then
        {

            if (this.gameObject.tag == "NPC") // checks to see if the radius is tagged with the NPC tag.
            {
                Debug.Log("NPC1");
                flowchart.ExecuteBlock("Quest Dialogue"); // we execute the named block within the flowchart.
            }

            else if (this.gameObject.tag == "NPC2") // checks tag.
            {
                Debug.Log("Npc2");
                flowchart.ExecuteBlock("Testing1"); // we execute the named block within the flowchart.
            }

            else if (this.gameObject.tag == "NPC3")
            {
                Debug.Log("Npc3");
                flowchart.ExecuteBlock("FlavorHW"); // we execute the named block within the flowchart.
            }


            else if (this.gameObject.tag == "NPC4")
            {
                Debug.Log("Npc4");
                flowchart.ExecuteBlock("FlavorC"); // we execute the named block within the flowchart.
            }

            else if (this.gameObject.tag == "NPC5")
            {
                Debug.Log("Npc5");
                flowchart.ExecuteBlock("FlavorG"); // we execute the named block within the flowchart.
            }

            else if (this.gameObject.tag == "NPC6")
            {
                Debug.Log("NPC6");
                flowchart.ExecuteBlock("fireQuest"); // executing the fire quest chain.
            }

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
    private void TaskAgree()
    {
        TurnOnTaskObjects();

        pauseMenuReference.AddToOngoingList(questType);
    }

        private void TurnOnTaskObjects()
    {
        for (int counter = 0; counter < taskRelatedGameObject.Length; counter++)
        {
            taskRelatedGameObject[counter].SetActive(true);
        }
    }
}
