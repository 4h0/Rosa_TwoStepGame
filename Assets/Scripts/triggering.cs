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

    void OnTriggerEnter(Collider col)


    {
        if (col.gameObject.name=="cube")
        {

            // Debug.Log("hello");
            h = 1;
            // Debug.Log(h);
        }
    }

    void Start()
    {

        door = GameObject.Find("door");
        box = GameObject.Find("box");





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

        // }


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