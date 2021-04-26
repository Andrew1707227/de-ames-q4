using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //Holds if the game is paused
    public static bool gameIsPaused = false;

    //Holds reference to pause menu UI
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        //Checks for Cancel input
        if (Input.GetButtonDown("Cancel"))
        {
            //if the game is paused
            if (gameIsPaused)
            {
                //Calls Resume method
                Resume();
            }
            else
            {
                //Calls Pause method
                Pause();
            }
        }
    }

    public void Resume()
    {
        //Turns off pause menu UI
        pauseMenuUI.SetActive(false);
        //Starts time
        Time.timeScale = 1f;
        //Sets gameIsPaused to false
        gameIsPaused = false;
    }

    void Pause()
    {
        //Turns on pause menu UI
        pauseMenuUI.SetActive(true);
        //Stops time
        Time.timeScale = 0f;
        //Sets gameIsPaused to true
        gameIsPaused = true;
    }

    public void LoadCredits()
    {
        //Resets Time
        Time.timeScale = 1f;
        //Sets gameIsPaused to true
        gameIsPaused = false;
        //Loads Main Menu Scene
        SceneManager.LoadScene("CreditsScene");
    }    

    public void LoadMenu()
    {
        //Resets Time
        Time.timeScale = 1f;
        //Sets gameIsPaused to true
        gameIsPaused = false;
        //Loads Main Menu Scene
        SceneManager.LoadScene("TitleScene");
    }
}
