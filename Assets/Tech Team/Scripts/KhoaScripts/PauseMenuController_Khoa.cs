using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController_Khoa : MonoBehaviour
{
    public Sprite[] taskIcon;

    public GameObject[] rightSide, taskList;
    public Image[] leftSide;

    public Image[] onGoingTasks, completedTasks;
    public Image[] optionList, optionChoice;



    public GameObject pauseMenu;



    private PlayerController_Alex playerReference;
    private SoundVolumeUpdate_Khoa soundControllerReference;
    private Color verticalResetColor, verticalChosenColor;



    public string[] taskDescription;
    public bool[] alreadyHadThisTask;
    public int[] completedCount;



    private int[] currentVerticalUIScrolling, maxVerticalUIScrolling;



    public bool pauseMenuOn;
    public float soundVolume;



    private bool inputTaken, soundChange, optionChoosing, completedListCheck;
    private int pauseMenuOnandOff, soundOnandOff, currentHorizontalUIScrolling, maxHorizontalUIScrolling;
    private float upDownTimer, leftRightTimer;



    private List<int> onGoingList, completedList;



    private void Awake()
    {
        while (playerReference == null)
        {
            playerReference = FindObjectOfType<PlayerController_Alex>();
        }

        while (soundControllerReference == null)
        {
            soundControllerReference = FindObjectOfType<SoundVolumeUpdate_Khoa>();
        }

        onGoingList = new List<int>();
        completedList = new List<int>();

        TurnOffPauseMenu();
    }



    private void Start()
    {
        pauseMenuOn = false;
        inputTaken = false;
        optionChoosing = false;
        soundChange = false;
        pauseMenuOnandOff = 0;
        soundOnandOff = 0;
        currentHorizontalUIScrolling = 0;
        upDownTimer = 0;
        leftRightTimer = 0;
        soundVolume = 1;

        alreadyHadThisTask = new bool[] { false, false, false};
        completedCount = new int[] { 0, 0, 0};
        currentVerticalUIScrolling = new int[] { 0, 0, 0};
        maxVerticalUIScrolling = new int[] { 2, 0, 0};

        verticalResetColor = Color.white;
        verticalResetColor.a = .36f;
        verticalChosenColor = Color.red;
        verticalChosenColor.a = .36f;
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

        currentVerticalUIScrolling = new int[] {0, 0, 0};
        currentHorizontalUIScrolling = 0;

        playerReference.canMove = true;
        pauseMenuOn = false;
        optionChoosing = false;
        soundChange = false;

        Time.timeScale = 1f;
        soundOnandOff++;

        ResetUI();
        SoundOff();
    }



    private void InputCheck()
    {
        if (!soundChange)
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
                currentVerticalUIScrolling[1] = 0;
                leftRightTimer = 0;
                currentHorizontalUIScrolling--;
            }
            if (Input.GetKeyUp(KeyCode.D) || upDownTimer >= 1)
            {
                inputTaken = true;
                currentVerticalUIScrolling[1] = 0;
                leftRightTimer = 0;
                currentHorizontalUIScrolling++;
            }

            if (currentHorizontalUIScrolling < 0)
            {
                currentHorizontalUIScrolling = 0;
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
                        maxHorizontalUIScrolling = 1;
                        break;
                    }
            }
            if (currentHorizontalUIScrolling > maxHorizontalUIScrolling)
            {
                currentHorizontalUIScrolling = maxHorizontalUIScrolling;
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
                currentVerticalUIScrolling[currentHorizontalUIScrolling]--;
            }
            if (Input.GetKeyUp(KeyCode.S) || upDownTimer >= 1)
            {
                inputTaken = true;
                upDownTimer = 0;
                currentVerticalUIScrolling[currentHorizontalUIScrolling]++;
            }

            if (currentVerticalUIScrolling[currentHorizontalUIScrolling] < 0)
            {
                currentVerticalUIScrolling[currentHorizontalUIScrolling] = 0;
            }
            if (currentVerticalUIScrolling[currentHorizontalUIScrolling] > maxVerticalUIScrolling[currentHorizontalUIScrolling])
            {
                currentVerticalUIScrolling[currentHorizontalUIScrolling] = maxVerticalUIScrolling[currentHorizontalUIScrolling];
            }

            if (inputTaken)
            {
                ResetUI();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                soundVolume -= .03f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                soundVolume += .03f;
            }

            if (soundVolume < 0)
            {
                soundVolume = 0;
            }
            if (soundVolume > 1)
            {
                soundVolume = 1;
            }

            SoundOn();
        }

        if (optionChoosing && Input.GetButtonDown("Interact"))
        {
            switch(currentVerticalUIScrolling[1])
            {
                case 0:
                    {
                        Resume();
                        break;
                    }
                case 1:
                    {
                        Restart();
                        break;
                    }
                case 2:
                    {
                        Quit();
                        break;
                    }
                case 3:
                    {
                        soundOnandOff++;
                        if (soundOnandOff % 2 == 0)
                        {
                            SoundOff();
                        }
                        else
                        {
                            SoundOn();
                        }
                        break;
                    }
            }
        }
    }



    private void ResetUI()
    {
        inputTaken = false;
        optionChoosing = false;

        if (currentHorizontalUIScrolling == 0)
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
            if (onGoingList.Count > 0)
            {
                maxVerticalUIScrolling[1] = onGoingList.Count - 1;
            }
                

            foreach (GameObject tempGameObject in taskList)
            {
                tempGameObject.GetComponent<Image>().color = verticalResetColor;
            }

            if (currentHorizontalUIScrolling == 1)
            {
                foreach (Image tempImage1 in onGoingTasks)
                {
                    tempImage1.enabled = false;
                    tempImage1.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    tempImage1.transform.GetChild(0).GetChild(0).GetComponent<Text>().enabled = false;
                }

                OngoingListUIUpdate();
            }
            if(currentHorizontalUIScrolling == 2)
            {
                foreach (Image tempImage2 in completedTasks)
                {
                    tempImage2.enabled = false;
                    tempImage2.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    tempImage2.transform.GetChild(0).GetChild(0).GetComponent<Text>().enabled = false;
                }

                CompletedListUIUpdate();
            }

            for (int counter = 1; counter < taskList.Length; counter++)
            {
                taskList[counter].SetActive(false);
            }
        }

        if(currentVerticalUIScrolling[0] == 2)
        {
            maxVerticalUIScrolling[1] = 3;

            foreach (Image tempImage3 in optionList)
            {
                tempImage3.color = verticalResetColor;
            }

            foreach (Image tempImage4 in optionChoice)
            {
                tempImage4.color = Color.white;
            }
        }

        UpdateUI();
    }
    private void UpdateUI()
    {
        if (currentHorizontalUIScrolling == 0)
        {
            leftSide[currentVerticalUIScrolling[currentHorizontalUIScrolling]].color = Color.black;
            rightSide[currentVerticalUIScrolling[currentHorizontalUIScrolling]].SetActive(true);
        }

        if (currentVerticalUIScrolling[0] == 1)
        {
            rightSide[1].SetActive(true);

            taskList[currentHorizontalUIScrolling].GetComponent<Image>().color = verticalChosenColor;

            taskList[currentHorizontalUIScrolling].SetActive(true);

            if(currentHorizontalUIScrolling == 1)
            {
                OngoingListUIUpdate();
            }
            if(currentHorizontalUIScrolling == 2)
            {
                CompletedListUIUpdate();
            }
        }

        if (currentVerticalUIScrolling[0] == 2)
        {
            optionList[currentHorizontalUIScrolling].GetComponent<Image>().color = verticalChosenColor;

            if (currentHorizontalUIScrolling == 1)
            {
                UpdateOptionUI();
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
        alreadyHadThisTask[questType] = true;
        onGoingList.Add(questType);
    }
    public void RemoveFromOngoingList(int questType)
    {
        alreadyHadThisTask[questType] = false;

        for (int counter = 0; counter < onGoingList.Count; counter++)
        {
            if (onGoingList[counter] == questType)
            {
                onGoingList.Remove(onGoingList[counter]);

                break;
            }
        }
    }
    public void AddToCompletedList(int questType)
    {
        completedListCheck = false;

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



    private void UpdateOptionUI()
    {
        optionChoosing = true;

        optionChoice[currentVerticalUIScrolling[currentHorizontalUIScrolling]].color = Color.black;
    }

    private void Resume()
    {
        pauseMenuOnandOff++;

        TurnOffPauseMenu();
    }
    private void Restart()
    {
        SceneManager.LoadScene("RoseAnya");
    }
    private void Quit()
    {
        Application.Quit();
    }

    private void SoundOn()
    {
        soundChange = true;

        optionChoice[3].GetComponent<Image>().enabled = false;
        optionChoice[3].transform.GetChild(0).GetComponent<Image>().fillAmount = soundVolume / 1;
        optionChoice[3].transform.GetChild(0).GetComponent<Image>().enabled = true;
    }
    private void SoundOff()
    {
        soundChange = false;

        soundControllerReference.VolumeChange(soundVolume);

        optionChoice[3].GetComponent<Image>().enabled = true;
        optionChoice[3].transform.GetChild(0).GetComponent<Image>().enabled = false;
    }
}

