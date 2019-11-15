using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveFireElement : MonoBehaviour
{
    ///////// SETUP /////////
    // trail particle effect //
    public GameObject ParticleEffect;
    public GameObject player;
    bool PlayerIsInRadiusCheck;
    /////////////////////////
    void Start()
    {
        // Grabs PlayerElements script from player //
        PlayerElements player_elements = player.GetComponent<PlayerElements>();
        PlayerIsInRadiusCheck = false;
    }
    void Update()
    {
        PlayerIsInRadius();
    }

    ///////// FUNCTIONS /////////

    // Checks if player enters the radius //
    void OnTriggerEnter(Collider other) 
    { 
         if (other.tag == "Player") 
         {
            PlayerIsInRadiusCheck = true;
         }
    }
    //Checks if player exits the radius //
    void OnTriggerExit(Collider other)
    {
        PlayerIsInRadiusCheck = false;
    }
    void PlayerIsInRadius()
    {
        if (PlayerIsInRadiusCheck == true && Input.GetKeyDown(KeyCode.Q))
        {  
            GivePlayerFire();
        }
    }
    void GivePlayerFire()
    {
        // points to PlayerElement script and is changing the bool value //
        PlayerElements.FireElement = true;
        // Shoot particles at player //
        ParticleEffect.transform.position = player.transform.position;
    }
}
