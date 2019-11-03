using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuControl : MonoBehaviour
{
    public Transform canvas;
    public static bool GamePaused;

    void Start()
    {
        GamePaused = false;
    }
         
    void Update() 
    {
        Pause();
        Resume();
    }

    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
             
            if (canvas.gameObject.activeInHierarchy == false) 
            {        
                canvas.gameObject.SetActive (true);
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            } 
            else 
            {
                canvas.gameObject.SetActive (false);
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        } 
    }
    public void Resume()
    {
        if (GamePaused == true)
        {
            canvas.gameObject.SetActive (false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            GamePaused = false;
        }
    }
 
}
