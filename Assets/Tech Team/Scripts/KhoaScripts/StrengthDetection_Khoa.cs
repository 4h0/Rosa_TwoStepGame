using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthDetection_Khoa : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PickUp")
        {
            this.transform.parent.GetChild(0).GetComponent<Quest2_Khoa>().StopSideQuest2();
        }
    }
}
