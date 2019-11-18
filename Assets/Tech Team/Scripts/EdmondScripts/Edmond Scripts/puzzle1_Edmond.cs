using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzle1_Edmond : MonoBehaviour
{//variables for puzzle 1
    private GameObject door;
    private int h = 0;
    private float speed = 0;


    //This is the ontriggerenter function

        void OnTriggerEnter(Collider use)
    {
        if (use.gameObject.name == "cube")
            h = 1;



    }


    // Start is called before the first frame update
    void Start()
    {
        door = GameObject.Find("door");
    }

    // Update is called once per frame
    void Update()
    {
        if (h == 1)
        {
        speed = 19;
        door.transform.Translate(-Vector3.right * speed * Time.deltaTime);
        }
    }


}
