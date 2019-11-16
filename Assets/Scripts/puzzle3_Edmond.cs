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
    private List<int> values;
    private bool one;
    private bool two;
    private bool three;
    private bool abc;
    private int collector=0;
   // private int[] correct = new int[] { 1, 2, 3 };
    private bool start = true;
    private List<int> corrected = new List<int> { 1, 2, 3 };
    private int[] ordered = new int[3];




    //checks distance between two objects.
    private float[] distance1=new float[3];
  





    // Start is called before the first frame update
    void Start()
    {
        box1 = GameObject.Find("simonsays1");
        box2 = GameObject.Find("simonsays2");
        box3 = GameObject.Find("simonsays3");
        Player = GameObject.Find("Player");
        values = new List<int>();
    }



    // Update is called once per frame
    void Update()
    {
        if (start != false)
        {

            //updates if players touches a cube
            distance1[0] = Vector3.Distance(box1.transform.position, Player.transform.position);
            distance1[1] = Vector3.Distance(box2.transform.position, Player.transform.position);
            distance1[2] = Vector3.Distance(box3.transform.position, Player.transform.position);

            if (Input.GetKeyDown("r"))
            {
                //for (int a = 0; a < 3; a++)
                //{ 
                if (distance1[0] <= 102)
                {
                    values.Add(1);
                    check1 = true;
                    box1.GetComponent<Renderer>().material = blue;
                }

                else if (distance1[1] <= 102)
                {

                    values.Add(3);
                    check2 = true;
                    box2.GetComponent<Renderer>().material = blue;

                }
                else if (distance1[2] <= 102)
                {

                    values.Add(2);
                    check3 = true;
                    box3.GetComponent<Renderer>().material = blue;

                }

                // }




            }
            if (check1 == true && check2 == true && check3 == true)
            {
                
               
                //  for (int c=0; c<3; c++)
                // {
                // if (ordered[c]==correct[c])
                // {

                //collector+=1;
                // }
                abc = values.SequenceEqual(corrected);

               // }


                

                if (abc)
                {
                    box1.GetComponent<Renderer>().material = red;
                    box2.GetComponent<Renderer>().material = red;
                    box3.GetComponent<Renderer>().material = red;
                    start = false;
                    Debug.Log("You are correct");
                }


                else
                    Invoke("wrong", 1f);

            }
        }
    }















    




















    //wrong invoke function
    void wrong()
    {
        check1 = false;
        check2 = false;
        check3 = false;
        start = true;


        box1.GetComponent<Renderer>().material = original;
        box2.GetComponent<Renderer>().material = original;
        box3.GetComponent<Renderer>().material = original;
        values.Clear();



    }


   void once()
    {
        


        








    }







}




  


