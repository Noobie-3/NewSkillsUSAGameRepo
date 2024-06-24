using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutButtons : MonoBehaviour
{
    public GameObject[] instructionScreens;
    private int currentScreenIndex = 0;
    public GameObject MainScene;
    public GameObject TutScreen;
    public GameObject[] buttons;
    
    void Start()
    {
        // Disable all screens except the first one
        for (int i = 1; i < instructionScreens.Length; i++)
        {
            instructionScreens[i].SetActive(false);
        }
        Time.timeScale = 0f;
        GameController.instance.IsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        MainScene.SetActive(true);
    }

    public void NextScreen()
    {
        // Disable current screen
        print(instructionScreens[currentScreenIndex]);

        instructionScreens[currentScreenIndex].SetActive(false);

        // Move to the next screen
        currentScreenIndex++;

        // If there are more screens, enable the next one
        if (currentScreenIndex < instructionScreens.Length)
        {
            instructionScreens[currentScreenIndex].SetActive(true);
        }
        else
        {
            // All screens shown, start the game or show a start button
            StartGame();
            TutScreen.SetActive(false);
        }
    }


    public void PrevScreen()
    {
        // Disable current screen
        instructionScreens[currentScreenIndex].SetActive(false);

        // Move to the next screen\
        if(currentScreenIndex > instructionScreens.Length - (instructionScreens.Length -1))
        {
            currentScreenIndex--;

        }

        // If there are more screens, enable the prev one
        if (currentScreenIndex < instructionScreens.Length)
        {
            instructionScreens[currentScreenIndex].SetActive(true);
        }
    }
    void StartGame()
    {
        currentScreenIndex = 0;
        Time.timeScale = 1;
        GameController.instance.IsPaused =false;
        MainScene.SetActive(true);
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        for (int i = 0; i < instructionScreens.Length; i++)
        {
            instructionScreens[i].SetActive(true);
        }
    }
}




