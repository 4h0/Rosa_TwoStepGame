using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController_Khoa : MonoBehaviour
{
    public Sprite[] taskIcon;

    public GameObject[] rightSide, taskList;
    public Image[] leftSide;

    public GameObject[] completedCountPanel;
    public Image[] onGoingTasks, completedTasks;
    public Text[] onGoingTaskDescription;

    public GameObject pauseMenu;

    private PlayerController_Alex playerReference;

    public string[] taskDescription;
    public bool[] alreadyHadThisTask;
    public int[] completedCount;

    private int[] currentVerticalUIScrolling, maxVerticalUIScrolling;

    public bool pauseMenuOn, inputTaken;

    private int pauseMenuOnandOff, currentHorizontalScrolling, maxHorizontalUIScrolling;
    private float upDownTimer, leftRightTimer;

    private List<int> onGoingList, completedList;

    private void Awake()
    {
        playerReference = FindObjectOfType<PlayerController_Alex>();

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

        UpdateUI();
    }
    private void TurnOffPauseMenu()
    {
        pauseMenu.SetActive(false);

        currentVerticalUIScrolling = new int[] { 0, 0, 0 };
        currentHorizontalScrolling = 0;

        playerReference.canMove = true;
        pauseMenuOn = false;

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

            foreach (Image tempImage1 in onGoingTasks)
            {
                tempImage1.color = Color.white;
                tempImage1.enabled = false;
            }
            foreach(Text tempText1 in onGoingTaskDescription)
            {
                tempText1.enabled = false;
            }
            foreach (Image tempImage2 in completedTasks)
            {
                tempImage2.color = Color.white;
                tempImage2.enabled = false;
            }
            foreach (GameObject tempGameObject2 in completedCountPanel)
            {
                tempGameObject2.SetActive(false);
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
                CompletedUIUpdate();
            }
        }
    }

    private void OngoingListUIUpdate()
    {
        for(int counter = 0; counter < onGoingTasks.Length; counter++)
        {
            Debug.Log("counter " + counter + "currentVerticalUIScrolling" + currentVerticalUIScrolling[1]);
            if(counter > maxVerticalUIScrolling[1])
            {
                break;
            }

            if (currentVerticalUIScrolling[1] < 4)
            {
                onGoingTasks[currentVerticalUIScrolling[1]].color = Color.red;
                onGoingTasks[counter].sprite = taskIcon[onGoingList[counter]];
                onGoingTaskDescription[counter].text = taskDescription[onGoingList[counter]];
            }
            else
            {
                onGoingTasks[3].color = Color.red;
                onGoingTasks[counter].sprite = taskIcon[onGoingList[counter + currentVerticalUIScrolling[1] - 3]];
                onGoingTaskDescription[counter].text = taskDescription[onGoingList[counter + currentVerticalUIScrolling[1] - 3]];
            }

            onGoingTasks[counter].enabled = true;
            onGoingTaskDescription[counter].enabled = true;
        }
    }
    private void CompletedUIUpdate()
    {
    }

    public void AddToOngoingList(int questType)
    {
        alreadyHadThisTask[questType] = true;
        onGoingList.Add(questType);

        if (onGoingList.Count > 1)
        {
            maxVerticalUIScrolling[1]++;
        }
    }
    public void AddToCompletedList(int questType)
    {
        alreadyHadThisTask[questType] = false;

        if (completedList.Count > 1)
        {
            completedCount[questType]++;
        }

        for(int counter = 0; counter < onGoingList.Count; counter++)
        {
            if(onGoingList[counter] == questType)
            {
                onGoingList.Remove(onGoingList[counter]);
                maxVerticalUIScrolling[1]--;

                break;
            }
        }

        for(int counter2 = 0; counter2 < completedList.Count; counter2++)
        {
            if(completedList[counter2] != questType)
            {
                completedList.Add(questType);
                maxVerticalUIScrolling[2]++;
            }
        }
    }
}
