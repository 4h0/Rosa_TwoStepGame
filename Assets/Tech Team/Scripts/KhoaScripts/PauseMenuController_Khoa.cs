using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController_Khoa : MonoBehaviour
{
    public Sprite[] taskIcon;

    public GameObject[] rightSide, taskList;
    public Image[] leftSide;

    public Image[] onGoingTasks, completedTasks;

    public GameObject pauseMenu;

    private PlayerController_Alex playerReference;
    private PuzzleActivateCondition_Khoa puzzleActivateConditionRerference;

    public string[] taskDescription;
    public bool[] alreadyHadThisTask;
    public int[] completedCount;

    private int[] currentVerticalUIScrolling, maxVerticalUIScrolling;

    public bool pauseMenuOn;

    private bool inputTaken, completedListCheck;
    private int pauseMenuOnandOff, currentHorizontalScrolling, maxHorizontalUIScrolling;
    private float upDownTimer, leftRightTimer;

    private List<int> onGoingList, completedList;

    private void Awake()
    {
        playerReference = FindObjectOfType<PlayerController_Alex>();
        puzzleActivateConditionRerference = FindObjectOfType<PuzzleActivateCondition_Khoa>();

        onGoingList = new List<int>();
        completedList = new List<int>();

        TurnOffPauseMenu();
    }

    private void Start()
    {
        pauseMenuOn = false;
        inputTaken = false;
        pauseMenuOnandOff = 0;
        currentHorizontalScrolling = 0;
        upDownTimer = 0;
        leftRightTimer = 0;

        alreadyHadThisTask = new bool[] { false, false, false, false, false, false, false };
        completedCount = new int[] { 0, 0, 0, 0, 0, 0, 0 };
        currentVerticalUIScrolling = new int[] { 0, 0, 0 };
        maxVerticalUIScrolling = new int[] { 2, 0, 0 };
    }

    private void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            if(pauseMenuOnandOff % 2 == 0)
            {
                TurnOnPauseMenu();
            }
            else
            {
                TurnOffPauseMenu();
            }

            pauseMenuOnandOff++; 
        }

        if(pauseMenuOn)
        {
            InputCheck();
        }
    }

    private void TurnOnPauseMenu()
    {
        pauseMenu.SetActive(true);

        playerReference.canMove = false;
        pauseMenuOn = true;

        Time.timeScale = 0f;

        UpdateUI();
    }
    private void TurnOffPauseMenu()
    {
        pauseMenu.SetActive(false);

        currentVerticalUIScrolling = new int[] { 0, 0, 0 };
        currentHorizontalScrolling = 0;

        playerReference.canMove = true;
        pauseMenuOn = false;

        Time.timeScale = 1f;

        ResetUI();
    }

    private void InputCheck()
    {
        //horizontal input check
        if (Input.GetKey(KeyCode.A))
        {
            leftRightTimer -= .03f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            leftRightTimer += .03f;
        }

        if (Input.GetKeyUp(KeyCode.A) || upDownTimer <= -1)
        {
            inputTaken = true;
            leftRightTimer = 0;
            currentHorizontalScrolling--;
        }
        if (Input.GetKeyUp(KeyCode.D) || upDownTimer >= 1)
        {
            inputTaken = true;
            leftRightTimer = 0;
            currentHorizontalScrolling++;
        }

        if (currentHorizontalScrolling < 0)
        {
            currentHorizontalScrolling = 0;
        }
        switch (currentVerticalUIScrolling[0])
        {
            case 0:
                {
                    maxHorizontalUIScrolling = 0;
                    break;
                }
            case 1:
                {
                    maxHorizontalUIScrolling = 2;
                    break;
                }
            case 2:
                {
                    maxHorizontalUIScrolling = 0;
                    break;
                }
        }
        if (currentHorizontalScrolling > maxHorizontalUIScrolling)
        {
            currentHorizontalScrolling = maxHorizontalUIScrolling;
        }



        //vertical input check
        if (Input.GetKey(KeyCode.W))
        {
            upDownTimer -= .03f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            upDownTimer += .03f;
        }

        if (Input.GetKeyUp(KeyCode.W) || upDownTimer <= -1)
        {
            inputTaken = true;
            upDownTimer = 0;
            currentVerticalUIScrolling[currentHorizontalScrolling]--;
        }
        if (Input.GetKeyUp(KeyCode.S) || upDownTimer >= 1)
        {
            inputTaken = true;
            upDownTimer = 0;
            currentVerticalUIScrolling[currentHorizontalScrolling]++;
        }

        if (currentVerticalUIScrolling[currentHorizontalScrolling] < 0)
        {
            currentVerticalUIScrolling[currentHorizontalScrolling] = 0;
        }
        if (currentVerticalUIScrolling[currentHorizontalScrolling] > maxVerticalUIScrolling[currentHorizontalScrolling])
        {
            currentVerticalUIScrolling[currentHorizontalScrolling] = maxVerticalUIScrolling[currentHorizontalScrolling];
        }

        if (inputTaken)
        {
            ResetUI();
        }
    }

    private void ResetUI()
    {
        inputTaken = false;

        if (currentHorizontalScrolling == 0)
        {
            foreach (GameObject gameObject in rightSide)
            {
                gameObject.SetActive(false);
            }

            foreach (Image image in leftSide)
            {
                image.color = Color.white;
            }
        }

        if (currentVerticalUIScrolling[0] == 1)
        {
            foreach (GameObject tempGameObject in taskList)
            {
                Color tempColor = Color.white;
                tempColor.a = .36f;
                tempGameObject.GetComponent<Image>().color = tempColor;
            }

            if (currentHorizontalScrolling == 1)
            {
                foreach (Image tempImage1 in onGoingTasks)
                {
                    tempImage1.enabled = false;
                    tempImage1.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    tempImage1.transform.GetChild(0).GetChild(0).GetComponent<Text>().enabled = false;
                }

                OngoingListUIUpdate();
            }
            if(currentHorizontalScrolling == 2)
            {
                foreach (Image tempImage2 in completedTasks)
                {
                    tempImage2.enabled = false;
                    tempImage2.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    tempImage2.transform.GetChild(0).GetChild(0).GetComponent<Text>().enabled = false;
                }

                CompletedListUIUpdate();
            }
        }

        for (int counter = 1; counter < taskList.Length; counter++)
        {
            taskList[counter].SetActive(false);
        }

        UpdateUI();
    }
    private void UpdateUI()
    {
        if (currentHorizontalScrolling == 0)
        {
            leftSide[currentVerticalUIScrolling[currentHorizontalScrolling]].color = Color.black;
            rightSide[currentVerticalUIScrolling[currentHorizontalScrolling]].SetActive(true);
        }

        if (currentVerticalUIScrolling[0] == 1)
        {
            rightSide[1].SetActive(true);

            Color tempColor = Color.red;
            tempColor.a = .36f;
            taskList[currentHorizontalScrolling].GetComponent<Image>().color = tempColor;

            taskList[currentHorizontalScrolling].SetActive(true);

            if(currentHorizontalScrolling == 1)
            {
                OngoingListUIUpdate();
            }
            if(currentHorizontalScrolling == 2)
            {
                CompletedListUIUpdate();
            }
        }
    }

    private void OngoingListUIUpdate()
    {
        int tempCounter;

        if (onGoingList.Count > 0)
        {
            for (int counter = 0; counter < onGoingTasks.Length; counter++)
            {
                if (counter > maxVerticalUIScrolling[1])
                {
                    break;
                }

                if (currentVerticalUIScrolling[1] < 4)
                {
                    tempCounter = counter;
                }
                else
                {
                    tempCounter = counter + currentVerticalUIScrolling[1] - 3;
                }

                onGoingTasks[counter].enabled = false;
                onGoingTasks[counter].transform.GetChild(0).GetComponent<Image>().enabled = false;
                onGoingTasks[counter].transform.GetChild(0).GetChild(0).GetComponent<Text>().enabled = false;

                onGoingTasks[counter].sprite = taskIcon[onGoingList[tempCounter]];

                onGoingTasks[counter].transform.GetChild(0).GetComponent<Image>().color = Color.black;
                onGoingTasks[counter].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = taskDescription[onGoingList[tempCounter]];

                onGoingTasks[counter].transform.GetChild(0).GetComponent<Image>().enabled = true;
                onGoingTasks[counter].transform.GetChild(0).GetChild(0).GetComponent<Text>().enabled = true;
                onGoingTasks[counter].enabled = true;
            }

            if (currentVerticalUIScrolling[1] < 4)
            {
                onGoingTasks[currentVerticalUIScrolling[1]].transform.GetChild(0).GetComponent<Image>().color = Color.red;
            }
            else
            {
                onGoingTasks[3].transform.GetChild(0).GetComponent<Image>().color = Color.red;
            }
        }
    }
    private void CompletedListUIUpdate()
    {
        int tempCounter2;

        if (completedList.Count > 0)
        {
            for (int counterSecond = 0; counterSecond < completedTasks.Length; counterSecond++)
            {
                if (counterSecond > maxVerticalUIScrolling[2])
                {
                    break;
                }

                if (currentVerticalUIScrolling[2] < 4)
                {
                    tempCounter2 = counterSecond;
                }
                else
                {
                    tempCounter2 = counterSecond + currentVerticalUIScrolling[1] - 3;
                }

                completedTasks[counterSecond].enabled = false;
                completedTasks[counterSecond].transform.GetChild(0).GetComponent<Image>().enabled = false;
                completedTasks[counterSecond].transform.GetChild(0).GetChild(0).GetComponent<Text>().enabled = false;

                completedTasks[counterSecond].sprite = taskIcon[completedList[tempCounter2]];

                switch (completedCount[completedList[counterSecond]])
                {
                    case 0:
                        {
                            break;
                        }
                    case 1:
                        {
                            completedTasks[counterSecond].GetComponent<Image>().color = Color.grey;
                            break;
                        }
                    case 2:
                        {
                            completedTasks[counterSecond].GetComponent<Image>().color = Color.green;
                            break;
                        }
                    case 3:
                        {
                            completedTasks[counterSecond].GetComponent<Image>().color = Color.blue;
                            break;
                        }
                    case 4:
                        {
                            completedTasks[counterSecond].GetComponent<Image>().color = Color.yellow;
                            break;
                        }
                }

                completedTasks[counterSecond].transform.GetChild(0).GetComponent<Image>().color = Color.black;
                completedTasks[counterSecond].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = taskDescription[completedList[tempCounter2]] + " (" + completedCount[completedList[counterSecond]] + ")";

                completedTasks[counterSecond].transform.GetChild(0).GetComponent<Image>().enabled = true;
                completedTasks[counterSecond].transform.GetChild(0).GetChild(0).GetComponent<Text>().enabled = true;
                completedTasks[counterSecond].enabled = true;
            }

            if (currentVerticalUIScrolling[1] < 4)
            {
                completedTasks[currentVerticalUIScrolling[2]].transform.GetChild(0).GetComponent<Image>().color = Color.red;
            }
            else
            {
                completedTasks[3].transform.GetChild(0).GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void AddToOngoingList(int questType)
    {
        Debug.Log("baka" + questType);
        alreadyHadThisTask[questType] = true;
        onGoingList.Add(questType);

        if (onGoingList.Count > 1)
        {
            maxVerticalUIScrolling[1]++;
        }
    }
    public void RemoveFromOngoingList(int questType)
    {
        alreadyHadThisTask[questType] = false;

        for (int counter = 0; counter < onGoingList.Count; counter++)
        {
            if (onGoingList[counter] == questType)
            {
                if (onGoingList.Count > 1)
                {
                    maxVerticalUIScrolling[1]--;
                }

                onGoingList.Remove(onGoingList[counter]);

                break;
            }
        }
    }
    public void AddToCompletedList(int questType)
    {
        completedListCheck = false;
        puzzleActivateConditionRerference.PuzzleActivationCheck();

        if (completedList.Count > 0)
        {
            for (int counter2 = 0; counter2 < completedList.Count; counter2++)
            {
                if (completedList[counter2] != questType)
                {
                    completedListCheck = true;
                }
            }

            if(completedListCheck)
            {
                completedList.Add(questType);
                maxVerticalUIScrolling[2]++;
            }
        }
        else
        {
            completedList.Add(questType);
        }

        RemoveFromOngoingList(questType);

        if (completedCount[questType] < 4)
        {
            completedCount[questType]++;
        }

        alreadyHadThisTask[questType] = false;
    }
}
