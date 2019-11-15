using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController_Khoa : MonoBehaviour
{
    public GameObject[] rightSide;
    public Image[] leftSide;

    public GameObject pauseMenu;

    private PlayerController_Alex playerReference;

    public bool[] alreadyHadThisTask;

    public bool pauseMenuOn;

    private int pauseMenuOnandOff, whichList;

    private void Awake()
    {
        playerReference = FindObjectOfType<PlayerController_Alex>();

        TurnOffPauseMenu();
    }

    private void Start()
    {
        pauseMenuOn = false;
        pauseMenuOnandOff = 0;
        whichList = 0;
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
    }
    private void TurnOffPauseMenu()
    {
        pauseMenu.SetActive(false);

        foreach(Image image in leftSide)
        {
            image.color = Color.white;
        }

        playerReference.canMove = true;
        pauseMenuOn = false;
    }

    private void InputCheck()
    {
        RightSideOff();
        leftSide[whichList].color = Color.white;

        if (Input.GetKeyDown(KeyCode.W))
        {
            whichList--;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            whichList++;
        }

        if (whichList < 0)
        {
            whichList = 2;
        }
        if (whichList > 2)
        {
            whichList = 0;
        }

        RightSideOn();
        leftSide[whichList].color = Color.black;
    }

    private void RightSideOff()
    {
        foreach(GameObject gameObject in rightSide)
        {
            gameObject.SetActive(false);
        }
    }
    private void RightSideOn()
    {
        rightSide[whichList].SetActive(true);
    }
}
