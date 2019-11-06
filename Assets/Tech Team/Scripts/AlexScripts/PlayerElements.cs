using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElements : MonoBehaviour
{
    ///////// SETUP /////////
    // this bool indicates if the player has fire//
    public static bool FireElement;
    public static bool WaterElement;
    public static bool StrengthElement;
    public static bool AirElement;
    /////////////////////////
    void Start()
    {
        FireElement = false;
        WaterElement = false;
        StrengthElement = false;
        AirElement = false;
    }
    void Update()
    {
        
    }
}
