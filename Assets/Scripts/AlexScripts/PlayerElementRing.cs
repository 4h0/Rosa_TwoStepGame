using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElementRing : MonoBehaviour
{
    ///////// SETUP /////////
    public GameObject FireRingParticleEffect;
    /////////////////////////
    void Start()
    {
        PlayerElements player_elements = GetComponent<PlayerElements>();
    }
    void Update()
    {
        Checker();
    }

    ///////// FUNCTIONS /////////

    void Checker()
    {
        if (PlayerElements.FireElement)
        {
            StartCoroutine(PlayFireRing());
        }
    }
    IEnumerator PlayFireRing()
    {
        FireRingParticleEffect.SetActive (true);

        //Wait for 3 seconds
        yield return new WaitForSeconds(2);

        FireRingParticleEffect.SetActive (false);
    }
}
