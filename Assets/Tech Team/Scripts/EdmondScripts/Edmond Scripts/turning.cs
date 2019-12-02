using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turning : MonoBehaviour
{
    private bool done = false;
    public Material blue;
    public GameObject myPrefab;
    public GameObject cube;
    public GameObject sphere;

    void OnTriggerEnter(Collider use)
    {
        if (use.gameObject.name == "Player")
        {
            done = true;










           

        }




    }








    // Start is called before the first frame update
    void Start()
    {
        cube = GameObject.Find("changeobject");
        sphere.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (done)
        { 
        if (Input.GetKeyDown("r"))
        {


            //Instantiate(myPrefab, new Vector3(1000, 179, 774), Quaternion.identity);
                myPrefab.SetActive(true);
            Destroy(cube);
            Debug.Log("use it");
            done = false;
                sphere.transform.position = new Vector3(932, 179, 774);





        }


        }
















    }
}
