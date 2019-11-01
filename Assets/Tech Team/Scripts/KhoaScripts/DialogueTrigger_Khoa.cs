using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger_Khoa : MonoBehaviour
{
    public GameObject dialogueCanvas;
    public Text nameText;
    public Text dialogueText;

    public string charaName;

    public string[] sentences;

    private bool speaking;
    private int whichSentence;

    private void Start()
    {
        dialogueCanvas.SetActive(false);
        nameText.enabled = false;
        dialogueText.enabled = false;

        speaking = false;
        whichSentence = 0;
    }

    private void Update()
    {
        if(speaking)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                StartDialogue();
            }
        }
    }

    private void StartDialogue()
    {
        dialogueText.text = sentences[whichSentence];
        dialogueCanvas.SetActive(true);
        nameText.enabled = true;
        dialogueText.enabled = true;

        if (whichSentence < sentences.Length - 1)
        {
            whichSentence++;
        }
        else
        {
            whichSentence = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            speaking = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            dialogueCanvas.SetActive(false);
            nameText.enabled = false;
            dialogueText.enabled = false;
            speaking = false;
            whichSentence = 0;
        }
    }
}