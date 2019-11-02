using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggering : MonoBehaviour
{
    public int h=0;
    private GameObject door;
    // Start is called before the first frame update
    private Vector3 target=new Vector3(1097, 263, 50);
    

    void OnTriggerEnter(Collider col)


    {
        if (col.gameObject.name=="cube")
        {

            Debug.Log("hello");
            h = 1;
            Debug.Log(h);
        }
    }

    void Start()
    {

        door = GameObject.Find("door");
        




    }

   


    void Update()
    {
if (h==1)
        {
            float speed = 40;
            Debug.Log("yellow");
            //door.transform.Translate((Vector3.forward* 9)*(Time.deltaTime));
            door.transform.position = Vector3.MoveTowards(door.transform.position, target, speed * Time.deltaTime);
            

        }


    }

   

    
}