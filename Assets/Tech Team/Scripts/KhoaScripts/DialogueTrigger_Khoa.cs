using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger_Khoa : MonoBehaviour
{
    public GameObject dialoguePanel, yesNoPanel;
    public Text nameText, dialogueText, taskComplete;

    public GameObject[] taskRelatedGameObject;
    public Image[] noYesOption;

    private PlayerController_Alex playerReference;
    private PauseMenuController_Khoa pauseMenuReference;

    public string charaName;
    public int questType;

    public string[] sentences;

    private bool speaking, choosing;
    private int whichSentence, whichOption;

    private void Awake()
    {
        playerReference = FindObjectOfType<PlayerController_Alex>();
        pauseMenuReference = FindObjectOfType<PauseMenuController_Khoa>();

        nameText.text = charaName;

        DeactivateDialogue();
        DeactivateYesNoPanel();
        AssignTaskObjectParent();
        TurnOffTaskObjects();
    }

    private void Update()
    {
        if (!pauseMenuReference.pauseMenuOn)
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
    }

    private void AssignTaskObjectParent()
    {
        foreach (GameObject taskObject in taskRelatedGameObject)
        {
            taskObject.transform.SetParent(this.gameObject.transform);
        }
    }

    private void TurnOnTaskObjects()
    {
        for (int counter = 0; counter < taskRelatedGameObject.Length; counter++)
        {
            taskRelatedGameObject[counter].SetActive(true);
        }
    }
    public void TurnOffTaskObjects()
    {
        for (int counter = 0; counter < taskRelatedGameObject.Length; counter++)
        {
            taskRelatedGameObject[counter].SetActive(false);
        }
    }

    private void UpdateDialogueText()
    {
        dialogueText.text = sentences[whichSentence];
    }
    private void ResetYesNoUI()
    {
        foreach (Image image in noYesOption)
        {
            image.color = Color.white;
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
        ResetYesNoUI();

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
        choosing = true;

        yesNoPanel.SetActive(true);

        dialogueText.text = pauseMenuReference.taskDescription[questType];
    }
    private void DeactivateYesNoPanel()
    {
        playerReference.canMove = true;
        choosing = false;

        yesNoPanel.SetActive(false);

        ResetYesNoUI();
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

        pauseMenuReference.AddToOngoingList(questType);
    }
    public void TaskCompleted()
    {
        TurnOffTaskObjects();

        pauseMenuReference.AddToCompletedList(questType);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!pauseMenuReference.alreadyHadThisTask[questType])
            {
                UpdateDialogueText();
            }
            else
            {
                dialogueText.text = pauseMenuReference.taskDescription[pauseMenuReference.taskDescription.Length - 1];
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