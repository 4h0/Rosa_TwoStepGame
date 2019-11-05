using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController_Khoa : MonoBehaviour
{
    public Image dashMultiplier;
    public Image[] element;

    private PlayerController_Alex playerControllerReference;

    private void Awake()
    {
        foreach (Image image in element)
        {
            image.fillAmount = 0;
            image.enabled = true;
        }

        playerControllerReference = FindObjectOfType<PlayerController_Alex>();
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
        element[whichElement].fillAmount = playerControllerReference.elementalList[whichElement] / playerControllerReference.maxElementCounter;
        Debug.Log(element[whichElement].fillAmount);
    }
}
