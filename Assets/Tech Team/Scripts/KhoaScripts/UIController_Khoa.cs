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
