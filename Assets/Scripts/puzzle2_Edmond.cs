using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puzzle2_Edmond : MonoBehaviour
{//variables
    public Text doner;
    public Material red;
    public Material blue;
    public Text wincondition;
    private float timer = 12f; 
    private float current = 0f;





    private bool yos;
    private bool ab;

    private bool times = true;


    // Start is called before the first frame update
    void Start()
    {
        doner = GameObject.Find("countdown").GetComponent<Text>();
        wincondition = GameObject.Find("Winner").GetComponent<Text>();
        doner.enabled = false;
        wincondition.enabled = false;
        current = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (ab==true)
        {


            if (current >= 0 && yos != true)
            {
                doner.enabled = true;
                current -= 1 * Time.deltaTime;

                doner.text = current.ToString("0");
                



            }

            if (current<= 0 || yos == true)
            {
                Debug.Log("Winner");
                Timer();
                ab = false;
                
            }


        }





    }


    //Starts a timer function
    void Timer()
    {


        if (yos == true && current > 0)
        {
            Debug.Log("you win");
            Invoke("Winner", 3f);
            


        }

        else
        {
            Debug.Log("You here");
            yos = false;
            current = 0;
            Invoke("lose", 1f);
        }


    }

    void Winner()
    {
        wincondition.enabled = true;



    }

    void lose()
    {
        wincondition.text = "You lost";
        wincondition.enabled = true;


    }


    //Start a onTriggerEnter event
    void OnTriggerEnter(Collider puzzle)
    {
       if (puzzle.gameObject.name=="position1")
        {
            ab = true;
            Debug.Log("Okay, you have 12 seconds to reach the other position.");

        }

       else if (puzzle.gameObject.name=="position2")
        {
            yos = true;
            Debug.Log("You have reached the end");
        }


    }



    //


}
