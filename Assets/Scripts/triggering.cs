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
    private float timer = 6f;
    private float current = 0f;
    private float order;

    //this block is for checking if the simon says puzzle3
    private bool check1;
    private bool check2;
    private bool check3;
    //checks arraylist
    int[] correct =new int[3] { 3, 1, 2 };
    int[] orderings = new int[3];
    private int abc;




        //
    
    private bool yos;
    private bool ab;
    void OnTriggerEnter(Collider col)


    {
        if (col.gameObject.name == "cube")
        {

            Debug.Log("hello");
            h = 1;
            // Debug.Log(h);
        }

        else if (col.gameObject.CompareTag("start"))
        {
            ab = true;
            Debug.Log("Okay, you have 10 seconds to reach the other position.");
        }
        else if (col.gameObject.name == "position2")
            yos = true;

        else if (col.gameObject.name == "simonsays1")
        { 
        Debug.Log("Light");
        check1 = true;
            orderings[2] = 3;
            Debug.Log(orderings[2]);
            check1 = true;
        }


        //check another object
        else if (col.gameObject.name == "simonsays2")
        {
            orderings[1] = 2;
            check2 = true;
            Debug.Log(orderings[1]);
        }
        //checked if the other box has bee lifted.

        else if (col.gameObject.name == "simonsays3")
        {
            orderings[0] = 1;
            check3 = true;
            Debug.Log(orderings[0]);

        }
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
            Debug.Log(current);
            if (current <= 0)
            {
                current = 0;
                Timer();

            }

        }
        //checks if the user checks all values;
        if (check1==true && check2==true && check3==true)
        {
            check1 = check2 = check3 = false;

            for (int a = 0; a < 3; a++)
            {
                int b=0;
                b++;
                if (correct[a] == orderings[a])
                {
                    abc++;
                Debug.Log(abc);
                }

            }
        if (abc == 3)
                Debug.Log("Congrulations, you win the game");

         else
            Debug.Log("You didn't put in the correct order");
            Debug.Log(abc+" is the number of times you answered the correct answer");

        }


       

    }



    void Timer()
    {
        

        if (yos == true && current != 0)
            Debug.Log("you win");

        else 
            Debug.Log("Sorry, you lose half of your elemetal gauge.");

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