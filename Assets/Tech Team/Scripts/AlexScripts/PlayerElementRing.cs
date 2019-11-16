using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElementRing : MonoBehaviour
{
    ///////// SETUP /////////
    public GameObject FireRingParticleEffect;
    // public GameObject WaterRingParticleEffect;
    // public GameObject StrengthRingParticleEffect;
    // public GameObject AirRingParticleEffect;
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
        if (PlayerElements.FireElement.fillAmount >= .5f)
        {
            StartCoroutine(PlayFireRing());
        }
        // if (PlayerElements.WaterElement)
        // {
            // StartCoroutine(PlayWaterRing());
        // }
        // if (PlayerElements.StrengthElement)
        // {
        //     StartCoroutine(PlayStrengthRing());
        // }
        // if (PlayerElements.AirElement)
        // {
        //     StartCoroutine(PlayAirRing());
        // }
    }
    IEnumerator PlayFireRing()
    {
        FireRingParticleEffect.SetActive (true);

        //Wait for 3 seconds
        yield return new WaitForSeconds(2);

        FireRingParticleEffect.SetActive (false);
    }

    // IEnumerator PlayWaterRing()
    // {
        // WaterRingParticleEffect.SetActive (true);
// 
        // Wait for 3 seconds
        // yield return new WaitForSeconds(2);
// 
        // WaterRingParticleEffect.SetActive (false);
    // }

    //     IEnumerator PlayStrengthRing()
    // {
    //     StrengthRingParticleEffect.SetActive (true);

    //     //Wait for 3 seconds
    //     yield return new WaitForSeconds(2);

    //     StrengthRingParticleEffect.SetActive (false);
    // }

    //     IEnumerator PlayAirRing()
    // {
    //     AirRingParticleEffect.SetActive (true);

    //     //Wait for 3 seconds
    //     yield return new WaitForSeconds(2);

    //     AirRingParticleEffect.SetActive (false);
    // }
}
