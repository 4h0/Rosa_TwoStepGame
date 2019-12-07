using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerElements : MonoBehaviour
{
    ///////// SETUP /////////
    // this bool indicates if the player has fire//
    public static Image FireElement;
    public Image FireBar;
    public static Image WaterElement;
    public Image WaterBar;
    public static bool StrengthElement;
    public static bool AirElement;
    /////////////////////////
    void Start()
    {
        FireElement = FireBar;
        FireElement.fillAmount = 0f;

        WaterElement = WaterBar;
        WaterElement.fillAmount = 0f;

        StrengthElement = false;

        AirElement = false;
    }
    void Update()
    {
        
    }
}
