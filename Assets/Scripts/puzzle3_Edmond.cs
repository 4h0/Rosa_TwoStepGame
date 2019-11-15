using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class puzzle3_Edmond : MonoBehaviour
{ //variables
    private GameObject box1;
    private GameObject box2;
    private GameObject box3;
    public Material red;
    private GameObject Player;
    public Material blue;
    public Material original;
    //this block is for checking if the simon says puzzle3
    private bool check1;
    private bool check2;
    private bool check3;
    //checks arraylist
    private List<int> orderings = new List<int>(3);
    private List<int> correctorder = new List<int>() { 1, 2, 3 };
    private bool start=true;


   

    //checks distance between two objects.
    public float[] distances = new float[3];





    // Start is called before the first frame update
    void Start()
    {
        box1 = GameObject.Find("simonsays1");
        box2 = GameObject.Find("simonsays2");
        box3 = GameObject.Find("simonsays3");
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (start != false)
        {

            //updates if players touches a cube
            distances[0] = Vector3.Distance(box1.transform.position, Player.transform.position);
            distances[1] = Vector3.Distance(box2.transform.position, Player.transform.position);
            distances[2] = Vector3.Distance(box3.transform.position, Player.transform.position);

            if (Input.GetKey("r"))

                once();

            if (check1==true && check2==true && check3==true)
            {
                bool abc = correctorder.SequenceEqual(orderings);
                //Debug.Log(orderings.ElementAt(2));
                if (abc==true)
                {
                    box1.GetComponent<Renderer>().material = red;
                    box2.GetComponent<Renderer>().material = red;
                    box3.GetComponent<Renderer>().material = red;
                    start = false;
                    Debug.Log("You are correct");
                }


                else
                    Invoke("wrong", 1f);
                Debug.Log(" " + orderings.Count);


            }

        }














    }















    //wrong invoke function
   public  void wrong()
    {
        check1 = false;
        check2 = false;
        check3 = false;
        start = true;


        box1.GetComponent<Renderer>().material = original;
        box2.GetComponent<Renderer>().material = original;
        box3.GetComponent<Renderer>().material = original;
        orderings.Clear();



    }


    void once()
    {
        {

            if (distances[0] <= 102)
            {

                orderings.Add(1);
                box1.GetComponent<Renderer>().material = blue;
                check1 = true;

            }

            if (distances[1] <= 102)
            {
                orderings.Add(2);
                box2.GetComponent<Renderer>().material = blue;
                check2 = true;
            }
            if (distances[2] <= 102)
            {
                orderings.Add(3);
                box3.GetComponent<Renderer>().material = blue;
                check3 = true;
            }
        }







    }

}


  


