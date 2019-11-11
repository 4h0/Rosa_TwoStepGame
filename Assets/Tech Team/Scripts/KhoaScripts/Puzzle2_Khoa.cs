using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2_Khoa : MonoBehaviour
{
    private UIController_Khoa UIReference;

    public int maxTime;

    private bool doOnce;
    private int currentTime;

    private void Awake()
    {
        UIReference = FindObjectOfType<UIController_Khoa>();
        this.GetComponent<MeshRenderer>().material.color = Color.red;

        doOnce = false;
        currentTime = maxTime;
    }

    IEnumerator TakingTimeOff()
    {
        UIReference.Puzzle2Start(currentTime);
        doOnce = true;
        currentTime--;

        yield return new WaitForSeconds(.1f);

        doOnce = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && !doOnce && UIReference.puzzle2CanStart)
        {
            StartCoroutine(TakingTimeOff());

            this.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (UIReference.puzzle2CanStart)
            {
                if(currentTime > 0)
                {
                    UIReference.WinTextUpdate();
                }

                StartCoroutine(UIReference.Puzzle2End());

                currentTime = maxTime;
                this.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
    }
}
