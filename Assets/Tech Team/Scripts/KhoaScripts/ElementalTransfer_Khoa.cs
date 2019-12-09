using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalTransfer_Khoa : MonoBehaviour
{
    public AudioSource absorbSound;



    public ParticleSystem[] particleList;



    private PlayerController_Alex playerReference;
    private UIController_Khoa uiReference;



    public bool isGiving;
    public bool stayInside, doOnce, playerParticleOff;
    public int elementType;



    private bool alreadyGave, finishedGrowing;



    private void Awake()
    {
        while (playerReference == null)
        {
            playerReference = FindObjectOfType<PlayerController_Alex>();
        }

        while (uiReference == null)
        {
            uiReference = FindObjectOfType<UIController_Khoa>();
        }

        absorbSound = GetComponent<AudioSource>();

        stayInside = false;
        doOnce = false;
    }



    private void Update()
    {
        if (!doOnce && stayInside && Input.GetButtonDown("Interact"))
        {
            if (isGiving)
            {
                switch(elementType)
                {
                    case 0:
                        {
                            GavePlayerFire();
                            break;
                        }
                    case 1:
                        {
                            break;
                        }
                    case 2:
                        {
                            break;
                        }
                    case 3:
                        {
                            break;
                        }
                }
            }
            else
            {
                if (!alreadyGave)
                {
                    switch (elementType)
                    {
                        case 0:
                            {
                                break;
                            }
                        case 1:
                            {
                                break;
                            }
                        case 2:
                            {
                                GetPlayerWater();
                                break;
                            }
                        case 3:
                            {
                                break;
                            }
                    }
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



    private void GavePlayerFire()
    {
        doOnce = true;

        if (!absorbSound.isPlaying)
        {
            absorbSound.Play();
        }
        else
        {
            absorbSound.Stop();
        }

        transform.parent.GetComponent<Quest1_Khoa>().Quest1ConditionCheck();
        playerReference.elementalList[elementType] = playerReference.maxElementCounter[elementType];
        uiReference.UpdateElement(elementType);

        playerReference.GetComponent<PlayerController_Alex>().TurnOnPlayerParticle();
        TurnOffParticle();

        doOnce = false;
    }



    private void GetPlayerWater()
    {
        doOnce = true;
        alreadyGave = true;

        TurnOnParticle();

        if (playerReference.elementalList[elementType] > 0)
        {
            playerReference.elementalList[elementType]--;
            uiReference.UpdateElement(elementType);

            StartCoroutine(GrowingTree());
        }
    }

    IEnumerator GrowingTree()
    {
        yield return new WaitForSeconds(.1f);

        if(this.transform.GetChild(0).transform.localScale.x < 1f)
        {
            this.transform.GetChild(0).transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);

            StartCoroutine(GrowingTree());
        }
        else
        {
            TurnOffParticle();

            transform.parent.GetComponent<Quest3_Khoa>().Quest3ConditionCheck();

            doOnce = false;

            yield break;
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

