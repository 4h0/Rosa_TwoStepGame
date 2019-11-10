using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveUseElement : MonoBehaviour
{
     ///////// SETUP /////////
    // public GameObject ParticleEffect;
    public GameObject player;
    bool PlayerIsInRadiusCheck;
    public ParticleSystem fire;
    /////////////////////////
    void Start()
    {
        // Grabs Player //
        player = GameObject.FindWithTag("Player");
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
        if (this.gameObject.tag == "FireSource")
        {
            if (PlayerIsInRadiusCheck == true && Input.GetKeyDown(KeyCode.Q))
            {  
                GivePlayerFire();
            }
        }
        if (this.gameObject.tag == "EmptyFireSource")
        {
            if (PlayerIsInRadiusCheck == true && Input.GetKeyDown(KeyCode.Q) && PlayerElements.FireElement.fillAmount >= .5f)
            {  
                TakePlayerFire();
            }
        }        
        



        if (this.gameObject.tag == "WaterSource")
        {
            if (PlayerIsInRadiusCheck == true && Input.GetKeyDown(KeyCode.Q))
            {  
                GivePlayerWater();
            }
        }
        if (this.gameObject.tag == "EmptyWaterSource")
        {
            if (PlayerIsInRadiusCheck == true && Input.GetKeyDown(KeyCode.Q))
            {  
                TakePlayerWater();
            }
        }        

    }
    void GivePlayerFire()
    {
        // points to PlayerElement script and is changing the bool value //
        PlayerElements.FireElement.fillAmount =+ .5f;
        // Shoot particles at player - not using but may revisit//
        // ParticleEffect.transform.position = player.transform.position;
    }
    void TakePlayerFire()
    {
        PlayerElements.FireElement.fillAmount =- .5f;

        Debug.Log("Took fire from player");

        fire.Play();
    }



    void GivePlayerWater()
    {
        // points to PlayerElement script and is changing the bool value //
        PlayerElements.WaterElement = true;
        // Shoot particles at player - not using but may revisit//
        // ParticleEffect.transform.position = player.transform.position;
    }
    void TakePlayerWater()
    {
        PlayerElements.WaterElement = false;

        Debug.Log("Took water from player");

    }
}
