using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest3_Khoa : MonoBehaviour
{
    public GameObject[] receivedElement;



    private PauseMenuController_Khoa pauseMenuReference;



    public bool quest3Pass;
    public int questType;



    private int totalNumberOfQuestObjects;



    private void Awake()
    {
        while (pauseMenuReference == null)
        {
            pauseMenuReference = FindObjectOfType<PauseMenuController_Khoa>();
        }

        foreach (GameObject tempGameObjects in receivedElement)
        {
            tempGameObjects.SetActive(false);
            tempGameObjects.transform.SetParent(this.transform, true);
        }

        totalNumberOfQuestObjects = receivedElement.Length;
    }



    public void StartSideQuest3()
    {
        pauseMenuReference.AddToOngoingList(questType);

        foreach (GameObject tempGameObjects in receivedElement)
        {
            tempGameObjects.SetActive(true);
        }
    }
    public void Quest3ConditionCheck()
    {
        totalNumberOfQuestObjects--;

        if(totalNumberOfQuestObjects <= 0)
        {
            StartCoroutine(JuicingKindOf());
        }
    }
    public void StopSideQuest3()
    {
        pauseMenuReference.RemoveFromOngoingList(questType);
        pauseMenuReference.AddToCompletedList(questType);

        foreach (GameObject tempGameObjects in receivedElement)
        {
            tempGameObjects.SetActive(false);
        }
        quest3Pass = true; //QUEST PASSED
    }

    IEnumerator JuicingKindOf()
    {
        yield return new WaitForSeconds(3f);

        StopSideQuest3();
    }
}
