using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggering : MonoBehaviour
{
    public int h=0;
    private GameObject door;
    // Start is called before the first frame update
    private Vector3 target=new Vector3(1097, 263, 50);
    private GameObject box;
    private float timer = 120f;
    private float current = 0f;
    
    private bool yos=false;
    private bool ab;
    void OnTriggerEnter(Collider col)


    {
        if (col.gameObject.name == "cube")
        {

           Debug.Log("hello");
            h = 1;
            // Debug.Log(h);
        }

        else if (col.gameObject.name == "position1")
        { 
        ab = true;
        Debug.Log("yellow");
        }
        else if (col.gameObject.name == "position2")
            yos= true;
    }

    void Start()
    {

        door = GameObject.Find("door");
        box = GameObject.Find("box");

        current = timer;



    }

   


    void Update()
    {
        if (h == 1)

        {
            // if (box.transform.position.z <= -38 && box.transform.position.z >= -94)

            // {

                // if (box.transform.position.x >= 273 && box.transform.position.x <= 310)
                // { 

                float speed = 10;
                // Debug.Log("yellow");

                door.transform.Translate(-Vector3.right * speed * Time.deltaTime);
            // }
        }

        if (ab)
        {

            current -= 1 * Time.deltaTime;

            if (current <= 0)
            {
                current = 0;
                Timer();

            }

        }


    }



    void Timer()
    {
        

        if (yos == true && current != 0)
            Debug.Log("you win");

        else 
            Debug.Log("yes");

    }



    //Debug.Log("done");
    // if(box.transform.position.z>=-38 && box.transform.position.z<=-94)
    // {

    //  if (box.transform.position.x>=267 && box.transform.position.x<=311)
    //  {
    //         Debug.Log("done");
    //         float speed = 40;
    //door.transform.position = Vector3.MoveTowards(door.transform.position, target.transform.position, speed);

    //   }

    // }

}