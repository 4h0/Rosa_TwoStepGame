using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus; // must be used. This allows the script to access the fungus scripts.

public class dialogueTrigger : MonoBehaviour
{
    public Text timerText;
    private Quest2 Quest2Script;
    public GameObject[] taskRelatedGameObjects;
    public Flowchart flowchart; // calls the flowchart.

    private PlayerController_Alex playerReference;
    private PauseMenuController_Khoa pauseMenuReference;
    private UIController_Khoa uiControllerReference;

    public int questType, maxTimer, currentTimer, objectsNumber;

    private bool hasPlayer, finishedTask, hasTalked; // is the player in a collider? yes or no

    private void Awake()
    {
        Quest2Script = FindObjectOfType<Quest2>();
        hasTalked = false;
        timerText.enabled = false;
        questType = flowchart.GetIntegerVariable("questType");

        playerReference = FindObjectOfType<PlayerController_Alex>();
        pauseMenuReference = FindObjectOfType<PauseMenuController_Khoa>();
        uiControllerReference = FindObjectOfType<UIController_Khoa>();

        foreach (GameObject tempGameObjects in taskRelatedGameObjects)
        {
            tempGameObjects.SetActive(false);
            tempGameObjects.transform.SetParent(this.transform, true);
        }

        objectsNumber = taskRelatedGameObjects.Length;
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
                    flowchart.ExecuteBlock("fireQuest"); // executing the fire quest chain.
                    hasTalked = true;
                }
            }
            else if (!finishedTask && hasTalked) // QUEST IN PROGRESS CHECKER!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            {
                if (this.gameObject.tag == "NPC6") //checks npc tag
                {
                    Debug.Log("Quest not done yet.");
                    flowchart.ExecuteBlock("IPfire"); // you know what this does by now :D
                }
            }
            else if (finishedTask && hasTalked) // checks if task is COMPLETED!!!!!!
            {
                if (this.gameObject.tag == "NPC6")
                {
                    Debug.Log("Quest complete.");
                    flowchart.ExecuteBlock("fFire");
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
                if (this.gameObject.tag == "NPC8")
                {
                    Debug.Log("NPC8");
                    flowchart.ExecuteBlock("fetchQuest");
                    hasTalked = true;
                }
                else if (this.gameObject.tag == "NPC9")
                {
                    Debug.Log("NPC9");
                    flowchart.ExecuteBlock("preFetch");
                }
            }
            else if (!finishedTask && hasTalked) // QUEST IN PROGRESS CHECKER!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            {
                if (this.gameObject.tag == "NPC8")
                {
                    Debug.Log("In progress NPC8");
                    flowchart.ExecuteBlock("IPfetch");
                }
                else if (this.gameObject.tag == "NPC9")
                {
                    hasTalked = true;
                    Debug.Log("In progress NPC9");
                    flowchart.ExecuteBlock("fetchSecondary");
                    // Quest2Script.Delivered();
                    
                }
            }
            else if (finishedTask && hasTalked) // checks if task is COMPLETED!!!!!!
            {
                if (this.gameObject.tag == "NPC8")
                {
                    Debug.Log("Quest NPC8 is done.");
                    flowchart.ExecuteBlock("fFetch");
                }
                else if (this.gameObject.tag == "NPC9")
                {
                    Debug.Log("Quest NPC9 is done.");
                    flowchart.ExecuteBlock("fetchSecondary");
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
                if (this.gameObject.tag == "NPC7")
                {
                    Debug.Log("NPC7");
                    flowchart.ExecuteBlock("strengthQuest");
                    hasTalked = true;
                }
            }
            else if (!finishedTask && hasTalked) // QUEST IN PROGRESS CHECKER!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            {
                if (this.gameObject.tag == "NPC7")
                {
                    Debug.Log("Strength Quest not done yet.");
                    flowchart.ExecuteBlock("IPstrength");
                }
            }
            else if (finishedTask && hasTalked) // checks if task is COMPLETED!!!!!!
            {
                if (this.gameObject.tag == "NPC7")
                {
                    Debug.Log("Strength Quest Complete");
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
    private void TaskAgree()
    {
        StartSideQuest();

        pauseMenuReference.AddToOngoingList(questType);
    }

    private void UpdateTimerText()
    {
        currentTimer--;
        timerText.text = currentTimer / 60 + " : " + currentTimer % 60;
        timerText.enabled = true;

        StartCoroutine(TimerCountDown());
    }

    IEnumerator TimerCountDown()
    {
        yield return new WaitForSeconds(.1f);

        if (finishedTask)
        {
            yield break;
        }
        else
        {
            if (currentTimer > 0)
            {
                UpdateTimerText();
            }
            else
            {
                playerReference.maxElementCounter[questType] /= 2;
                playerReference.elementalList[questType] = playerReference.maxElementCounter[questType];

                uiControllerReference.UpdateElement(questType);
                StartCoroutine(StopSideQuest());
            }
        }
    }

    public void StartSideQuest()
    {
        currentTimer = maxTimer;

        if (!pauseMenuReference.alreadyHadThisTask[questType])
        {
            foreach (GameObject tempGameObjects in taskRelatedGameObjects)
            {
                tempGameObjects.SetActive(true);
            }

            UpdateTimerText();
        }
    }
    public void QuestConditionCheck()
    {
        objectsNumber--;

        if (objectsNumber <= 0)
        {
            finishedTask = true;

            pauseMenuReference.AddToCompletedList(questType);
            StartCoroutine(StopSideQuest());
        }
    }

    IEnumerator StopSideQuest()
    {
        timerText.enabled = false;

        pauseMenuReference.RemoveFromOngoingList(questType);

        yield return new WaitForSeconds(3f);

        foreach (GameObject tempGameObjects in taskRelatedGameObjects)
        {
            tempGameObjects.SetActive(false);
        }
    }
}
