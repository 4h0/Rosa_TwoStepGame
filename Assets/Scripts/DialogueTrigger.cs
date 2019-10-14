using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Canvas dialogueCanvas;
    public Text nameText;
    public Text dialogueText;

    public string charaName;

    public string[] sentences;

    private bool speaking;
    private int whichSentence;

    void Start()
    {
        dialogueCanvas.enabled = false;
        nameText.enabled = false;
        dialogueText.enabled = false;

        whichSentence = 0;
    }

    private void StartDialogue()
    {
        speaking = true;

        dialogueText.text = sentences[whichSentence];

        if (whichSentence < sentences.Length -1)
        {
            whichSentence++;
        }
        else
        {
            whichSentence = 0;
        }

        StartCoroutine(WaitTimer());
    }

    IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(3f);

        speaking = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            nameText.text = charaName;
            dialogueCanvas.enabled = true;
            nameText.enabled = true;
            dialogueText.enabled = true;

            if (!speaking)
            {
                StartDialogue();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            dialogueCanvas.enabled = false;
            nameText.enabled = false;
            dialogueText.enabled = false;
            whichSentence = 0;
        }
    }
}