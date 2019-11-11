using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class colored : MonoBehaviour
{
    public Material red;
    public Material blue;



    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material = blue;


    }
    void OnTriggerEnter(Collider call)
    {

        if (call.gameObject.name == "Player")
        {
            GetComponent<Renderer>().material = red;
            Debug.Log("yos");
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
