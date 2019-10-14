using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalTransfer : MonoBehaviour
{

    public GameObject nextLevel;
    public ParticleSystem playerParticle;
    public ParticleSystem selfParticle;

    private PlayerController playerReference;

    public bool isGiving;
    public bool stayInside;

    private bool doOnce;
    private void Awake()
    {
        nextLevel.SetActive(false);

        playerReference = FindObjectOfType<PlayerController>();

        doOnce = false;
        stayInside = false;
    }

    private void Update()
    {
        if (!doOnce)
        {
            if (stayInside && Input.GetKeyDown(KeyCode.V))
            {
                if (isGiving)
                {
                    StartCoroutine(GavePlayer());
                }
                else
                {
                    if (playerReference.haveFire)
                    {
                        StartCoroutine(PlayerGave());
                    }
                }
            }
        }
    }

    IEnumerator GavePlayer()
    {
        doOnce = true;
        isGiving = false;
        playerReference.haveFire = true;
        selfParticle.Stop();
        playerParticle.Play();

        yield return new WaitForSeconds(6f);

        playerParticle.Stop();
        doOnce = false;
    }
    IEnumerator PlayerGave()
    {
        doOnce = true;
        isGiving = true;
        playerReference.haveFire = false;
        selfParticle.Play();
        playerParticle.Stop();
        nextLevel.SetActive(true);

        yield return new WaitForSeconds(6f);

        nextLevel.SetActive(false);
        doOnce = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            stayInside = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            stayInside = false;
        }
    }
}
