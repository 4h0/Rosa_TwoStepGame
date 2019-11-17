using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalTransfer_Khoa : MonoBehaviour
{
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
            ChangeColor();
        }
        else
        {
            this.GetComponent<MeshRenderer>().material.color = Color.white;
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

    private void ChangeColor()
    {
        switch (elementType)
        {
            case 0:
                {
                    this.GetComponent<MeshRenderer>().material.color = Color.red;
                    break;
                }
            case 1:
                {
                    this.GetComponent<MeshRenderer>().material.color = Color.yellow;
                    break;
                }
            case 2:
                {
                    this.GetComponent<MeshRenderer>().material.color = Color.blue;
                    break;
                }
            case 3:
                {
                    this.GetComponent<MeshRenderer>().material.color = Color.green;
                    break;
                }
        }
    }

    IEnumerator GavePlayer()
    {
        playerReference.elementalList[elementType] = playerReference.maxElementCounter[elementType];
        uiReference.UpdateElement(elementType);

        this.GetComponent<MeshRenderer>().material.color = Color.white;
        doOnce = true;

        yield return new WaitForSeconds(6f);

        ChangeColor();
        doOnce = false;
    }
    IEnumerator PlayerGave()
    {
        if (playerReference.elementalList[elementType] > 0)
        {
            playerReference.elementalList[elementType]--;
            uiReference.UpdateElement(elementType);
            this.transform.parent.GetComponent<DialogueTrigger_Khoa>().TaskCompleted();

            ChangeColor();
            doOnce = true;

            yield return new WaitForSeconds(6f);

            this.GetComponent<MeshRenderer>().material.color = Color.white;
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
