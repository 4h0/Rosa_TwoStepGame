using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest1_Khoa : MonoBehaviour
{
    public Text timerText;
    public GameObject[] taskRelatedGameObjects;

    private PlayerController_Alex playerReference;
    private PauseMenuController_Khoa pauseMenuReference;
    private UIController_Khoa uiControllerReference;

    public int questType, maxTimer, currentTimer, objectsNumber;

    private bool finishedTask;

    private void Awake()
    {
        timerText.enabled = false;

        playerReference = FindObjectOfType<PlayerController_Alex>();
        pauseMenuReference = FindObjectOfType<PauseMenuController_Khoa>();
        uiControllerReference = FindObjectOfType<UIController_Khoa>();

        foreach (GameObject tempGameObjects in taskRelatedGameObjects)
        {
            tempGameObjects.SetActive(false);
            tempGameObjects.transform.SetParent(this.transform, true);
            tempGameObjects.GetComponent<ElementalTransfer_Khoa>().ChangeQuestType(questType);
        }

        objectsNumber = taskRelatedGameObjects.Length;
    }



    private void UpdateTimerText()
    {
        currentTimer--;
        timerText.text = currentTimer / 60 + " : " + currentTimer % 60;
        timerText.enabled = true;

        StartCoroutine(TimerCountDown());
    }
    IEnumerator TimerCountDown()
    {
        yield return new WaitForSeconds(.1f);

        if (finishedTask)
        {
            yield break;
        }
        else
        {
            if (currentTimer > 0)
            {
                UpdateTimerText();
            }
            else
            {
                playerReference.maxElementCounter[questType] /= 2;

                if (playerReference.elementalList[questType] > playerReference.maxElementCounter[questType])
                {
                    playerReference.elementalList[questType] = playerReference.maxElementCounter[questType];
                }

                uiControllerReference.UpdateElement(questType);
                StartCoroutine(StopSideQuest1());
            }
        }
    }



    public void StartSideQuest1()
    {
        currentTimer = maxTimer;

        if (!pauseMenuReference.alreadyHadThisTask[questType])
        {
            pauseMenuReference.AddToOngoingList(questType);

            foreach (GameObject tempGameObjects in taskRelatedGameObjects)
            {
                tempGameObjects.SetActive(true);
            }

            UpdateTimerText();
        }
    }
    public void Quest1ConditionCheck()
    {
        objectsNumber--;

        if (objectsNumber <= 0)
        {
            finishedTask = true;

            pauseMenuReference.AddToCompletedList(questType);
            StartCoroutine(StopSideQuest1());
        }
        else
        {
            playerReference.GetComponent<PlayerController_Alex>().playerParticle.Stop();
        }

        this.transform.parent.GetComponent<dialogueTrigger>().taskDone = finishedTask;
    }

    IEnumerator StopPlayerParticle()
    {
        yield return new WaitForSeconds(2.4f);

        playerReference.GetComponent<PlayerController_Alex>().playerParticle.Stop();
    }
    IEnumerator StopSideQuest1()
    {
        timerText.enabled = false;

        pauseMenuReference.RemoveFromOngoingList(questType);

        yield return new WaitForSeconds(3f);

        foreach (GameObject tempGameObjects in taskRelatedGameObjects)
        {
            tempGameObjects.SetActive(false);
        }

        playerReference.GetComponent<PlayerController_Alex>().playerParticle.Stop();
    }
}
