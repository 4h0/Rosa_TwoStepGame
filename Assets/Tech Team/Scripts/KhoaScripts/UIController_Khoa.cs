using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController_Khoa : MonoBehaviour
{
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

        while (playerControllerReference == null)
        {
            playerControllerReference = FindObjectOfType<PlayerController_Alex>();
        }
    }



    private void Start()
    {
        for (int counter = 0; counter < 4; counter++)
        {
            UpdateElement(counter);
        }
    }



    public void UpdateElement(int whichElement)
    {
        element[whichElement].fillAmount = playerControllerReference.elementalList[whichElement] / playerControllerReference.maxElementCounter[whichElement];

        elementalText[whichElement].text = (playerControllerReference.maxElementCounter[whichElement]).ToString();
        elementalText[whichElement].enabled = true;
    }
}
