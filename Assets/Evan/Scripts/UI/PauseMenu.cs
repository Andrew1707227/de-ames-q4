using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PauseMenu : MonoBehaviour
{
    //Holds if the game is paused
    public static bool gameIsPaused = false;

    //Holds reference to pause menu UI
    public GameObject pauseMenuUI;

    //Holds wanted blur value
    public float wantedBlur = 1.37f;

    //Holds blur-er
    public GameObject globalVolume;

    Volume v;
    DepthOfField dOF;

    private void Start()
    {
        v = globalVolume.GetComponent<Volume>();
        v.profile.TryGet(out dOF);
    }

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
        //Resets blur
        dOF.focusDistance.value = 1.8f;

        //Turns off pause menu UI
        pauseMenuUI.SetActive(false);
        //Starts time
        Time.timeScale = 1f;
        //Sets gameIsPaused to false
        gameIsPaused = false;
    }

    void Pause()
    {
        //Start slow blur
        StartCoroutine(slowBlurIn());

        //Turns on pause menu UI
        pauseMenuUI.SetActive(true);
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

    private IEnumerator slowBlurIn()
    {
        //While blur is not low enough
        while (dOF.focusDistance.value > wantedBlur)
        {
            //Increment blur value
            dOF.focusDistance.value -= 0.025f;
            yield return new WaitForSeconds(0.01f); //Wait
        }
        //Set blure to wanted blur
        dOF.focusDistance.value = wantedBlur;

        //Turn off time
        Time.timeScale = 0f;
    }
}
