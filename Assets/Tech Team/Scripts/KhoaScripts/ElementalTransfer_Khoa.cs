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

    private bool alreadyGave;
    private int storedQuestType;

    private void Awake()
    {
        playerReference = FindObjectOfType<PlayerController_Alex>();
        uiReference = FindObjectOfType<UIController_Khoa>();
        absorbSound = GetComponent<AudioSource>();

        stayInside = false;
        doOnce = false;
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
        if (!doOnce && stayInside && Input.GetButtonDown("Interact"))
        {
            if (isGiving)
            {
                GavePlayer();
            }
            else
            {
                if (!alreadyGave)
                {
                    PlayerGave();
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


    public void ChangeQuestType(int questType)
    {
        storedQuestType = questType;
    }
    private void GavePlayer()
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

        if (storedQuestType == 0)
        {
            transform.parent.GetComponent<Quest1_Khoa>().Quest1ConditionCheck();
        }

        playerReference.elementalList[elementType] = playerReference.maxElementCounter[elementType];
        uiReference.UpdateElement(elementType);

        playerReference.GetComponent<PlayerController_Alex>().playerParticle.Play();
        TurnOffParticle();

        doOnce = false;
    }
    private void PlayerGave()
    {
        doOnce = true;
        alreadyGave = true;

        this.GetComponent<MeshRenderer>().material.color = Color.white;

        if (playerReference.elementalList[elementType] > 0)
        {
            playerReference.elementalList[elementType]--;
            uiReference.UpdateElement(elementType);

            TurnOnParticle();
        }

        if(storedQuestType == 2)
        {
            transform.parent.GetComponent<Quest3_Khoa>().Quest3ConditionCheck();
        }

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
