using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    Scene CurrentScene;
    string CurrentSceneName;
    public void Start()
    {
        CurrentScene = SceneManager.GetActiveScene();
        CurrentSceneName = CurrentScene.name;

        if (CurrentSceneName != "MainMenu")
        {
            GameObject player = GameObject.Find("Player");
            PlayerMenuControl player_menu_control = player.GetComponent<PlayerMenuControl>();
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("whitebox tech1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ResumeGame()
    {
        PlayerMenuControl.GamePaused = true;
    }
}
