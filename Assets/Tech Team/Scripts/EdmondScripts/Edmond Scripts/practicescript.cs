using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class practicescript : MonoBehaviour
{

    public GameObject box1;
    public GameObject box2;
    public GameObject box3;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider check)

    {
        if (check.gameObject.name == "cubepractice")
            Debug.Log("yellow");




    }
}
