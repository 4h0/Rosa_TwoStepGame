using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public void Startgame()
    {

        SceneManager.LoadScene("Edmond's Sanctuary");


    }


    public void Quit()
    {
        Debug.Log("Quit game");
        Application.Quit();
        
    }
}
