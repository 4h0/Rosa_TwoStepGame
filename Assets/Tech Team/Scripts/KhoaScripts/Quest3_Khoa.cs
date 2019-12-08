using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest3_Khoa : MonoBehaviour
{
    public GameObject[] receivedElement;

    private PauseMenuController_Khoa pauseMenuReference;

    public int questType;

    private int totalNumberOfQuestObjects;

    public bool quest3Pass;

    private void Awake()
    {
        pauseMenuReference = FindObjectOfType<PauseMenuController_Khoa>();

        foreach (GameObject tempGameObjects in receivedElement)
        {
            tempGameObjects.SetActive(false);
            tempGameObjects.transform.SetParent(this.transform, true);
            tempGameObjects.GetComponent<ElementalTransfer_Khoa>().ChangeQuestType(questType);
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
            StopSideQuest2();
        }
    }
    public void StopSideQuest2()
    {
        pauseMenuReference.RemoveFromOngoingList(questType);
        pauseMenuReference.AddToCompletedList(questType);

        foreach (GameObject tempGameObjects in receivedElement)
        {
            tempGameObjects.SetActive(false);
        }
        quest3Pass = true; //QUEST PASSED
    }
}
