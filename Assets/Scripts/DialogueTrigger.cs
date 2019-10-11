using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public string charaName;

    public string[] sentences;

    void Start()
    {
        nameText.enabled = false;
        dialogueText.enabled = false;

        Debug.Log(sentences.Length);
    }

    IEnumerator StartDialogue()
    {
        for(int counter = 0; counter < sentences.Length ; counter++)
        {
            dialogueText.text = sentences[counter];
            yield return new WaitForSeconds(3f); 
        }                
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            nameText.text = charaName;
            nameText.enabled = true;
            dialogueText.enabled = true;

            StartCoroutine(StartDialogue());
        }
    }
}