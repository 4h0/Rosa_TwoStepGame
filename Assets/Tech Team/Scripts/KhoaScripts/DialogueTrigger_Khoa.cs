using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger_Khoa : MonoBehaviour
{
    public GameObject dialoguePanel, yesNoPanel;
    public Text nameText, dialogueText;

    public Transform[] taskSpawnPlace;
    public Image[] noYesOption;

    private PlayerController_Alex playerReference;
    private PauseMenuController_Khoa pauseMenuReference;
    private List<GameObject> taskRelatedGameObject;

    public string charaName;
    public int questType;

    public string[] sentences, description;

    private bool speaking, choosing;
    private int whichSentence, whichOption;

    private void Awake()
    {
        playerReference = FindObjectOfType<PlayerController_Alex>();
        pauseMenuReference = FindObjectOfType<PauseMenuController_Khoa>();

        nameText.text = charaName;

        DeactivateDialogue();
        DeactivateYesNoPanel();
    }

    private void Start()
    {
        taskRelatedGameObject = new List<GameObject>();


        switch (questType)
        {
            case 0:
                {
                    taskRelatedGameObject.Add(pauseMenuReference.taskGameObjectsList[0]);
                    taskRelatedGameObject.Add(pauseMenuReference.taskGameObjectsList[1]);

                    break;
                }
            case 1:
                {
                    taskRelatedGameObject.Add(pauseMenuReference.taskGameObjectsList[2]);
                    taskRelatedGameObject.Add(pauseMenuReference.taskGameObjectsList[3]);

                    break;
                }
            case 2:
                {
                    taskRelatedGameObject.Add(pauseMenuReference.taskGameObjectsList[4]);
                    taskRelatedGameObject.Add(pauseMenuReference.taskGameObjectsList[5]);

                    break;
                }
            case 3:
                {
                    taskRelatedGameObject.Add(pauseMenuReference.taskGameObjectsList[6]);
                    taskRelatedGameObject.Add(pauseMenuReference.taskGameObjectsList[7]);

                    break;
                }
            case 4:
                {
                    taskRelatedGameObject.Add(pauseMenuReference.taskGameObjectsList[8]);
                    taskRelatedGameObject.Add(pauseMenuReference.taskGameObjectsList[9]);

                    break;
                }
            case 5:
                {
                    taskRelatedGameObject.Add(pauseMenuReference.taskGameObjectsList[10]);

                    break;
                }
        }

        for (int counter = 0; counter < taskRelatedGameObject.Count; counter++)
        {
            taskRelatedGameObject[counter].transform.position = taskSpawnPlace[counter].position;
            taskRelatedGameObject[counter].transform.rotation = taskSpawnPlace[counter].rotation;
        }

        TurnOffTaskObjects();
    }

    private void Update()
    {
        if (!choosing)
        {
            if (speaking)
            {
                if (Input.GetButtonDown("Interact"))
                {
                    DialogueCheck();
                }
            }
        }
        else
        {
            YesNoOptionCheck();

            if (Input.GetButtonDown("Interact"))
            {
                OptionChoice();
            }
        }
    }

    private void TurnOnTaskObjects()
    {
        for (int counter = 0; counter < taskRelatedGameObject.Count; counter++)
        {
            taskRelatedGameObject[counter].SetActive(true);
        }

        pauseMenuReference.alreadyHadThisTask[questType] = true;
    }
    private void TurnOffTaskObjects()
    {
        for (int counter = 0; counter < taskRelatedGameObject.Count; counter++)
        {
            taskRelatedGameObject[counter].SetActive(false);
        }
    }

    private void DialogueCheck()
    {
        if (whichSentence < sentences.Length - 1)
        {
            whichSentence++;

            dialogueText.text = sentences[whichSentence];
            ActivateDialogue();
        }
        else
        {
            ActivateYesNoPanel();
        }
    }
    private void YesNoOptionCheck()
    {
        noYesOption[whichOption].color = Color.white;

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S))
        {
            whichOption--;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W))
        {
            whichOption++;
        }

        if (whichOption < 0)
        {
            whichOption = 1;
        }
        if (whichOption > 1)
        {
            whichOption = 0;
        }

        noYesOption[whichOption].color = Color.black;
    }
    private void UpdateDialogueText()
    {
        dialogueText.text = sentences[whichSentence];
    }

    private void ActivateDialogue()
    {
        dialoguePanel.SetActive(true);
    }
    private void DeactivateDialogue()
    {
        dialoguePanel.SetActive(false);
        speaking = false;
        choosing = false;

        whichSentence = 0;
    }

    private void ActivateYesNoPanel()
    {
        playerReference.canMove = false;

        yesNoPanel.SetActive(true);

        choosing = true;
        dialogueText.text = description[questType];
    }
    private void DeactivateYesNoPanel()
    {
        playerReference.canMove = true;
        choosing = false;

        yesNoPanel.SetActive(false);
    }

    private void OptionChoice()
    {
        switch(whichOption)
        {
            case 0:
                {
                    DeactivateYesNoPanel();
                    UpdateDialogueText();
                    break;
                }
            case 1:
                {
                    TaskAgree();
                    break;
                }
        }
    }
    private void TaskAgree()
    {
        DeactivateDialogue();
        DeactivateYesNoPanel();
        TurnOnTaskObjects();

        pauseMenuReference.alreadyHadThisTask[questType] = true;
        dialogueText.text = description[description.Length - 1];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!pauseMenuReference.alreadyHadThisTask[questType])
            {
                UpdateDialogueText();
            }

            ActivateDialogue();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !pauseMenuReference.alreadyHadThisTask[questType])
        {
            speaking = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DeactivateDialogue();
            DeactivateYesNoPanel();
        }
    }
}