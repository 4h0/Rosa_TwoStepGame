using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image[] ability;
    public Text[] coolDown;

    private PlayerController playerControllerReference;

    private int coolDownNumber;

    private void Awake()
    {
        foreach(Image image in ability)
        {
            image.enabled = false;
        }

        foreach(Text text in coolDown)
        {
            text.enabled = false;
        }

        playerControllerReference = FindObjectOfType<PlayerController>();
    }    

    public void StartCoolDown(int whichAbility)
    {
        ability[whichAbility].enabled = true;
        coolDown[whichAbility * 2].enabled = true;
        coolDown[whichAbility * 2 + 1].enabled = true;
    }

    public void UpdateTime(int whichAbility, int timer)
    {
        coolDown[whichAbility * 2 + 1].text = timer.ToString();
    }

    public void StopCooling(int whichAbility)
    {
        ability[whichAbility].enabled = false;
        coolDown[whichAbility * 2].enabled = false;
        coolDown[whichAbility * 2 + 1].enabled = false;

    }
}
