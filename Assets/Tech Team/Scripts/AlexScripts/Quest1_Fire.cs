using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest1_Fire : MonoBehaviour
{
    private GameObject Fire0; 
    private GameObject Fire1;
    private GameObject Fire2;
    private GameObject Fire3;

    private Transform player;
    public Transform [] FireParticles;
    public float [] Dist;

    void Awake()
    {
        FireParticles[0] = GameObject.FindGameObjectWithTag("Fire1").transform;
        FireParticles[1] = GameObject.FindGameObjectWithTag("Fire2").transform;
        FireParticles[2] = GameObject.FindGameObjectWithTag("Fire3").transform;
        FireParticles[3] = GameObject.FindGameObjectWithTag("Fire4").transform;
        player = this.transform;

        // This turns off the fire in at the start of the game
        for (var i = 0; i < 4; i++)
        {
            FireParticles[i].transform.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // This will end up being replaced with whatever activates the quest
        if (Input.GetKeyDown(KeyCode.V))
        {
            Distance();
        }
    }

    void Distance()
    {
        // This turns on all the fire once the quest has been accepted
        for (var i = 0; i < 4; i++)
        {
            FireParticles[i].transform.gameObject.SetActive(true);
        }
        // Check distance from player and fire. Makes sure player is close enough to the fire
        for (var i = 0; i < 4; i++)
        {
            Dist[i] = Vector3.Distance(FireParticles[i].position, player.position);

            if (Dist[i] < 2)
            {
                Debug.Log("You're close");
            }
        }
    }
}