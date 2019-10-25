using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalTransfer : MonoBehaviour
{
    public ParticleSystem playerParticle;
    public ParticleSystem selfParticle;

    private PlayerController playerReference;
    private UIController uiReference;

    public bool isGiving;
    public bool stayInside;
    public int elementType;

    private bool doOnce;

    private void Awake()
    {
        playerReference = FindObjectOfType<PlayerController>();
        uiReference = FindObjectOfType<UIController>();

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
        if(playerReference.elementalList[elementType] < 4)
        {
            playerReference.elementalList[elementType]++;
            uiReference.UpdateElement(elementType);

            doOnce = true;
            playerReference.haveFire = true;
            selfParticle.Stop();
            playerParticle.Play();

            yield return new WaitForSeconds(6f);

            selfParticle.Play();
            playerParticle.Stop();
            doOnce = false;
        }
    }
    IEnumerator PlayerGave()
    {
        if (playerReference.elementalList[elementType] > 0)
        {
            playerReference.elementalList[elementType]--;
            uiReference.UpdateElement(elementType);

            doOnce = true;
            playerReference.haveFire = false;
            selfParticle.Play();
            playerParticle.Stop();

            yield return new WaitForSeconds(6f);

            doOnce = false;
        }
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
