using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalTransfer_Khoa : MonoBehaviour
{
    public ParticleSystem[] particleList;

    private PlayerController_Alex playerReference;
    private UIController_Khoa uiReference;

    public bool isGiving;
    public bool stayInside;
    public int elementType;

    private bool doOnce;

    private void Awake()
    {
        playerReference = FindObjectOfType<PlayerController_Alex>();
        uiReference = FindObjectOfType<UIController_Khoa>();

        doOnce = false;
        stayInside = false;
    }

    private void Start()
    {
        if (isGiving)
        {
            TurnOnParticle();
        }
        else
        {
            TurnOffParticle();
        }
    }

    private void Update()
    {
        if (!doOnce)
        {
            if (stayInside && Input.GetButtonDown("Interact"))
            {
                if (isGiving)
                {
                    StartCoroutine(GavePlayer());
                }
                else
                {
                    StartCoroutine(PlayerGave());
                }
            }
        }
    }

    private void TurnOnParticle()
    {
        foreach (ParticleSystem particleTemp in particleList)
        {
            particleTemp.Play();
        }
    }
    private void TurnOffParticle()
    {
        foreach (ParticleSystem particleTemp in particleList)
        {
            particleTemp.Stop();
        }
    }

    IEnumerator GavePlayer()
    {
        playerReference.elementalList[elementType] = playerReference.maxElementCounter[elementType];
        uiReference.UpdateElement(elementType);
        transform.parent.GetComponent<dialogueTrigger>().QuestConditionCheck();

        playerReference.GetComponent<PlayerController_Alex>().playerParticle.Play();
        TurnOffParticle();

        doOnce = true;

        yield return new WaitForSeconds(1.5f);

        playerReference.GetComponent<PlayerController_Alex>().playerParticle.Stop();
        doOnce = false;
    }
    IEnumerator PlayerGave()
    {
        if (playerReference.elementalList[elementType] > 0)
        {
            playerReference.elementalList[elementType]--;
            uiReference.UpdateElement(elementType);

            TurnOnParticle();

            doOnce = true;

            yield return new WaitForSeconds(.3f);

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
