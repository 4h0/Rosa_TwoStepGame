using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseFireElement : MonoBehaviour
{
    ///////// SETUP /////////
    bool PlayerIsInRadiusCheck;
    public ParticleSystem fire;
    public ParticleSystem FireRing;
    /////////////////////////
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        PlayerElements player_elements = player.GetComponent<PlayerElements>();
        PlayerIsInRadiusCheck = false;
        MoveFireRingUp move_fire_ring_up = FireRing.GetComponent<MoveFireRingUp>();
    }
    void Update()
    {
        PlayerIsInRadius();
    }

    ///////// FUNCTIONS /////////

    void OnTriggerEnter(Collider other) 
    {
         if (other.tag == "Player") 
         {
            PlayerIsInRadiusCheck = true;
         }
    }
    void OnTriggerExit(Collider other) 
    {
        if (other.tag == "Player") 
        {
           PlayerIsInRadiusCheck = false;
        }
    }
    void PlayerIsInRadius()
    {
        if (PlayerIsInRadiusCheck == true && Input.GetKeyDown(KeyCode.Q))
        {  
            TakePlayerFire();
        }
    }
    void TakePlayerFire()
    {
       // PlayerElements.FireElement = false;

        MoveFireRingUp.startPlaying = true;

        Debug.Log("test" + MoveFireRingUp.startPlaying);

        fire.Play();
    }

}
