using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerElements : MonoBehaviour
{
    ///////// SETUP /////////
    // this bool indicates if the player has fire//
    public static Image FireElement;
    public Image FireEl;
    public static bool WaterElement;
    public static bool StrengthElement;
    public static bool AirElement;
    /////////////////////////
    void Start()
    {
        FireElement = FireEl;
        FireElement.fillAmount = 0f;
        WaterElement = false;
        StrengthElement = false;
        AirElement = false;
    }
    void Update()
    {
        
    }
}
