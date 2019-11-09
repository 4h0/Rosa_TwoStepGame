﻿using System.Collections;
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
            GameObject playerfixed = GameObject.Find("PlayerFixed");
            PlayerMenuControl player_menu_control = playerfixed.GetComponent<PlayerMenuControl>();
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("WhiteBox Art");
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