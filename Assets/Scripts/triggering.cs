using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class triggering : MonoBehaviour
{
    public int h=0;
    private GameObject door;
    // Start is called before the first frame update
    private Vector3 target=new Vector3(1097, 263, 50);
    private GameObject box;
    private float timer = 12f;
    private float current = 0f;
    private bool times = true;
    private Renderer read;

    //For the colors
    private GameObject color1;
    private GameObject color2;
    private GameObject color3;

    //this block is for checking if the simon says puzzle3
    private bool check1;
    private bool check2;
    private bool check3;
    //checks arraylist
    private List<int> orderings = new List<int>();
    private List<int> correctorder = new List<int>() {1, 2, 3};
   
    private bool check=true;




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

        else if (col.gameObject.name=="position1")
        {
            ab = true;
            Debug.Log("Okay, you have 12 seconds to reach the other position.");
        }
        else if (col.gameObject.name == "position2")
            yos = true;

        else if (col.gameObject.name == "simonsays1")
        { 
        Debug.Log("Light");
        check1 = true;
            orderings.Add(3);   
            check1 = true;
            Debug.Log("You chose 3");
        }


        //check another object
        else if (col.gameObject.name == "simonsays2")
        {
            orderings.Add(1);
            check2 = true;
            Debug.Log("You chose 1");
            
        }
        //checked if the other box has bee lifted.

        else if (col.gameObject.name == "simonsays3")
        {
            orderings.Add(2);
            check3 = true;
            Debug.Log("You chose 2");

        }

        else if(col.gameObject.name=="position1")
        {

          


        }



        //checks if the user checks all values;
        if (check1 == true && check2 == true && check3 == true)
        {
            // foreach (int el in orderings)
            // Debug.Log(orderings[el]);
            // for (int a = 0; a < 3; a++)
            // {
            check1 = check2 = check3 = false;
            // }
            for (int al = 0; al < 3; al++)
            {
                if (orderings[al] != correctorder[al])
                {

                    check = false;
                }

                //Debug.Log("Correct "+correctorder[al]+ "userinput: "+orderings[al]);
                // yes++;
            }


            if (check == false)

                Debug.Log("no");

            else
                Debug.Log("yes");
        }


    }

    void Start()
    {

        door = GameObject.Find("door");
        box = GameObject.Find("box");

        color1 = GameObject.Find("position1");
        


    }

   


    void Update()
    {

        if (ab)
        {
            

            if (times == true && current >= 0.0f)
            {

                current -= 1*Time.deltaTime;
                Debug.Log("You have " + current + " seconds left");




            }

            else if (current <= 0)
            { 
            times = false;
            
            Debug.Log("You have 0 seconds left");
                Timer();

            }


        }

       















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

