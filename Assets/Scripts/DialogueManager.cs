using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;

    private Queue<string> sentences; // This keeps track of all the dialogue. DO NOT delete/remove System.Collections. 
    
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        Debug.Log("Starting dialogue with" + dialogue.name);

        nameText.text = dialogue.name;

        sentences.Clear(); // Clears any previous dialogue from the queue.

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() // calls the dialogue from the 'next' button.
    {
        if (sentences.Count == 0) // if there are no sentences left in the queue - end the dialogue.
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        Debug.Log("End of dialogue.");
    }

}
