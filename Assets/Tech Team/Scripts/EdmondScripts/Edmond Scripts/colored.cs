using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class colored : MonoBehaviour
{
    public Material red;
    public Material blue;

    private bool check1;

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
            
        }

    }
    // Update is called once per frame
    void Update()
    {
        

    }
}
