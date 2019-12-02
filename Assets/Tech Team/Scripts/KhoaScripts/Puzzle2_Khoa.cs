using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2_Khoa : MonoBehaviour
{
    private UIController_Khoa UIReference;
    
    public int maxTime;

    private bool doOnce, puzzle2CanStart;
    private int currentTime;

    private void Awake()
    {
        UIReference = FindObjectOfType<UIController_Khoa>();
        this.GetComponent<MeshRenderer>().material.color = Color.red;

        doOnce = false;
        puzzle2CanStart = true;
        currentTime = maxTime;
    }/*

    IEnumerator TakingTimeOff()
    {
        doOnce = true;
        currentTime--;
        if(currentTime > 0)
        {
            UIReference.Puzzle2TextUpdate(0, currentTime);
        }
        else
        {
            UIReference.Puzzle2TextUpdate(2, currentTime);

            StartCoroutine(Puzzle2End());
        }

        yield return new WaitForSeconds(.1f);

        doOnce = false;
    }

    public IEnumerator Puzzle2End()
    {
        puzzle2CanStart = false;

        yield return new WaitForSeconds(3f);

        UIReference.TurnOffTimerText();

        puzzle2CanStart = true;
        currentTime = maxTime;

        this.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && !doOnce && puzzle2CanStart)
        {
            StartCoroutine(TakingTimeOff());

            this.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }  
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && puzzle2CanStart)
        {
            if (currentTime > 0)
            {
                UIReference.Puzzle2TextUpdate(1, currentTime);
                this.transform.parent.GetComponent<DialogueTrigger_Khoa>().TaskCompleted();
            }
        }
    }
    */
}
