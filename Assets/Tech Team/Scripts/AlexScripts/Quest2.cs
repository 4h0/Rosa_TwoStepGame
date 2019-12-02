using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest2 : MonoBehaviour
{
    bool Quest2Active;
    private GameObject DeliverObject;
    private Rigidbody DeliverObjectRb;

    void Awake()
    {
        DeliverObject = GameObject.FindGameObjectWithTag("DeliverableItem");
        DeliverObjectRb = DeliverObject.GetComponent<Rigidbody>();
        DeliverObject.SetActive(false);
        Quest2Active = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Delivering()
    {
        DeliverObject.SetActive(true);
        DeliverObjectRb.angularVelocity = Random.insideUnitSphere;
    }
    public void Delivered()
    {
        DeliverObject.SetActive(false);
    }
}