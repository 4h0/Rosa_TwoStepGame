using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController_Khoa : MonoBehaviour
{
    public Text timerText;
    public Text[] elementalText;
    public Image dashMultiplier;
    public Image[] element;

    private PlayerController_Alex playerControllerReference;

    public bool puzzle2CanStart;

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
        puzzle2CanStart = true;
    }

    public void Puzzle2Start(int timer)
    {
        if (timer > 0)
        {
            timerText.text = (timer / 60).ToString() + " : " + (timer % 60).ToString();
        }
        else
        {
            timerText.text = "You Fail";

            int randomDeduction = Random.Range(0, 3);

            playerControllerReference.maxElementCounter[randomDeduction] /= 2;
            playerControllerReference.elementalList[randomDeduction] = playerControllerReference.maxElementCounter[randomDeduction];
            UpdateElement(randomDeduction);

            StartCoroutine(Puzzle2End());
        }

        timerText.enabled = true;
    }

    public void WinTextUpdate()
    {
        timerText.text = "You Win";
    }

    public IEnumerator Puzzle2End()
    {
        puzzle2CanStart = false;
        
        yield return new WaitForSeconds(3f);

        timerText.enabled = false;

        puzzle2CanStart = true;
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
