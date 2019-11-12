using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
public class triggering : MonoBehaviour
{
    //sets the color
    public Text doner;

    //own materials

   // public Material red;
   // public Material blue;

    private GameObject pos2;

    public Material red;
    public Material blue;




    public Text wincondition;

    Material material;
    public int h = 0;
    private GameObject door;
    // Start is called before the first frame update
    private Vector3 target = new Vector3(1097, 263, 50);
    private GameObject box;
    private float timer = 12f;
    private float current = 0f;
    private bool times = true;
    private GameObject[] samian= new GameObject[3];

    public int counter;

    //this is material


    private bool yes1;

    //For the colors
    private GameObject color1;

    //for renderer
    public Renderer rend;

    //this block is for checking if the simon says puzzle3
    private bool check1;
    private bool check2;
    private bool check3;
    //checks arraylist
    private List<int> orderings = new List<int>();
    private List<int> correctorder = new List<int>() { 1, 2, 3 };

    private bool check = true;




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

        else if (col.gameObject.name == "position1")
        {
            ab = true;
            Debug.Log("Okay, you have 12 seconds to reach the other position.");

        }
        else if (col.gameObject.name == "position2")
            yos = true;

        else if (col.gameObject.name == "simonsays1")
        {
            
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
            {

                for (int hey = 0; hey < 3; hey++)
                {
                    samian[hey].GetComponent<Renderer>().material = blue;
                    Debug.Log("incorrect");

                }




            }

            else
                Debug.Log("yes");
        }


    }

    void Start()
    {
        pos2 = GameObject.Find("position2");
        door = GameObject.Find("door");
        box = GameObject.Find("box");
        current = timer;
        doner = GameObject.Find("countdown").GetComponent<Text>();
        wincondition = GameObject.Find("Winner").GetComponent<Text>();
        doner.enabled = false;
        wincondition.enabled = false;
        samian = GameObject.FindGameObjectsWithTag("race");
        for (int abd = 0; abd < 3; abd++)
        {
            samian[abd].GetComponent<Renderer>().material = blue;


        }
    }




    void Update()
    {

        if (ab)
        {


            if (times == true && yos!=true)
            {
                doner.enabled = true;
                current -= 1 * Time.deltaTime;

                doner.text = current.ToString("0");




            }

            if (current <= 0|| yos==true)
            {
                times = false;
                
                yes1=true;
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

    void alert()
    {

        wincondition.enabled = true;
        Invoke("reset", 4f);

    }
void reset()
    {
        wincondition.enabled = false;



    }





    void Timer()
    {


        if (yos == true && current >= 0)
        {
            Debug.Log("you win");
            Invoke("alert", 3f);


        }

       else
        {
            yos = false;
            current = 0;
            Invoke("lose", 1f);
        }
            

    }

    //conditon to lose
    void lose()
    {
        wincondition.text = "You lost";
        wincondition.enabled = true;
        


    }

    void hey ()
    {
        wincondition.enabled = false;

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

