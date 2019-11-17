using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController_Khoa : MonoBehaviour
{
    public Text timerText;
    public Image dashMultiplier;

    public Text[] elementalText;
    public Image[] element;

    private PlayerController_Alex playerControllerReference;

    private void Awake()
    {
        foreach (Image image in element)
        {
            image.fillAmount = 0;
            image.enabled = true;
        }
        foreach (Text text in elementalText)
        {
            text.enabled = false;
        }

        playerControllerReference = FindObjectOfType<PlayerController_Alex>();

        timerText.enabled = false;
    }
    public void Puzzle2TextUpdate(int caseType, int timer)
    {
        switch (caseType)
        {
            case 0:
                {
                    timerText.text = (timer / 60).ToString() + " : " + (timer % 60).ToString();

                    break;
                }
            case 1:
                {
                    timerText.text = "You Completed Puzzle 2 Task";

                    break;
                }
            case 2:
                {
                    timerText.text = "You Fail";

                    int randomDeduction = Random.Range(0, 3);

                    playerControllerReference.maxElementCounter[randomDeduction] /= 2;
                    playerControllerReference.elementalList[randomDeduction] = playerControllerReference.maxElementCounter[randomDeduction];
                    UpdateElement(randomDeduction);

                    break;
                }
        }

        timerText.enabled = true;
    }
    public void TurnOffTimerText()
    {
        timerText.enabled = false;
    }

    public void DashMultiplierOn()
    {
        dashMultiplier.fillAmount = playerControllerReference.dashMultiplier / playerControllerReference.maxDashMultiplier;

        dashMultiplier.enabled = true;
    }
    public void DashMultiplierOff()
    {
        dashMultiplier.fillAmount = 0;

        dashMultiplier.enabled = false;
    }

    public void UpdateElement(int whichElement)
    {
        element[whichElement].fillAmount = playerControllerReference.elementalList[whichElement] / playerControllerReference.maxElementCounter[whichElement];

        elementalText[whichElement].text = (playerControllerReference.maxElementCounter[whichElement]).ToString();
        elementalText[whichElement].enabled = true;
    }
}
