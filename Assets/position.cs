using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class position : MonoBehaviour
{
    //variables
  
    private int timer = 120;
  
    private bool finish;
    private bool start;

    void OnTriggerEnter(Collider sas)
    {
        if (sas.gameObject.name=="position2")
        {


            finish=true;

        }


        if (sas.gameObject.name=="position1")
        {

            Debug.Log("hello");
            start =true;

        }






    }
    
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (start==true)
        {
            timer -= 1;

            if (finish==true && timer!=0)
            {

                Debug.Log("You win");
            }
        
            else if (finish==true && timer==0)
            {

                Debug.Log("You lose");
            }


        }

    }






   
}
