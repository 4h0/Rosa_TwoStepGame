using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest2_Khoa : MonoBehaviour
{
    public GameObject[] taskRelatedGameObjects;

    public GameObject taskReceiveNPC, parentNPC;

    private PauseMenuController_Khoa pauseMenuReference;

    public int questType;

    private void Awake()
    {
        pauseMenuReference = FindObjectOfType<PauseMenuController_Khoa>();

        foreach (GameObject tempGameObjects in taskRelatedGameObjects)
        {
            tempGameObjects.SetActive(false);
        }
    }



    public void StartSideQuest2()
    {
        if (!pauseMenuReference.alreadyHadThisTask[questType])
        {
            pauseMenuReference.AddToOngoingList(questType);

            taskRelatedGameObjects[1].transform.SetParent(taskReceiveNPC.transform, false);
            taskRelatedGameObjects[1].transform.localPosition = new Vector3(0, 0, 0);

            for (int counter = 0; counter < taskRelatedGameObjects.Length; counter++)
            {
                taskRelatedGameObjects[counter].SetActive(true);
                taskRelatedGameObjects[counter].transform.SetParent(parentNPC.transform, true);
            }
        }
    }
    public void StopSideQuest2()
    {
        pauseMenuReference.RemoveFromOngoingList(questType);
        pauseMenuReference.AddToCompletedList(questType);

        foreach (GameObject tempGameObjects in taskRelatedGameObjects)
        {
            tempGameObjects.SetActive(false);
        }
    }
}
