using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    //Holds if the game is paused
    public static bool gameIsPaused = false;

    //Holds wanted blur value
    public float wantedBlur = 1.37f;

    public float wantedPosX = 21.1f;
    public Vector2 defaultx = new Vector2(-100, 26.2f);

    public GameObject globalVolume;  //Holds blur-er
    public GameObject buttonHolder;  //Holds holder for pause menu buttons
    public GameObject pauseMenuUI;  //Holds reference to pause menu UI
    public GameObject checkScreenHolder;  //Holds the checkscreen
    public GameObject optionsScreen; //Hold the option menu
    public Canvas canvas;

    string location;

    AudioListener al;
    RectTransform rt;
    Volume v;
    DepthOfField dOF;

    private void Start()
    {
        al = gameObject.GetComponent<AudioListener>();

        v = globalVolume.GetComponent<Volume>();
        v.profile.TryGet(out dOF);

        rt = buttonHolder.GetComponent<RectTransform>();
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

    //Resumes game
    public void Resume()
    {
        if (Time.timeScale == 0f)
        {
            //Start sound up again
            AudioListener.pause = false;

            //Turn of check screen
            checkScreenHolder.SetActive(false);

            //Turn of options screen
            optionsScreen.SetActive(false);

            //Turn on time
            Time.timeScale = 1f;

            //Sets gameIsPaused to false
            gameIsPaused = false;

            //Start blur out and slide out
            StartCoroutine(slowBlurOut());
            StartCoroutine(slideOut());
        }
    }

    //Pauses game
    void Pause()
    {
        if (Time.timeScale == 1f)
        {
            //Pause sound
            AudioListener.pause = true;

            //Start slow blur and slide in
            StartCoroutine(slowBlurIn());
            StartCoroutine(slideIn());

            //Turns on pause menu UI
            pauseMenuUI.SetActive(true);
            //Sets gameIsPaused to true
            gameIsPaused = true;
        }
    }

    //Go to credits
    public void LoadCredits()
    {
        //Logs new location
        location = "CreditsScene";

        //Shows check screen
        checkScreenHolder.SetActive(true);
    }    

    //Go to menu
    public void LoadMenu()
    {
        //Logs new location
        location = "TitleScene";

        //Shows check screen
        checkScreenHolder.SetActive(true);
    }

    //Quit game
    public void Quit()
    {
        //Logs new location
        location = "Quit";

        //Shows check screen
        checkScreenHolder.SetActive(true);
    }

    //Loads logged loction on check accept
    public void checkAccept()
    {
        if (location == "Quit")
        {
            Debug.Log("Would of quit if built");
            Application.Quit();
        }
        else
        {
            //Start sound up again
            AudioListener.pause = false;
            //Resets Time
            Time.timeScale = 1f;
            //Sets gameIsPaused to true
            gameIsPaused = false;
            SceneManager.LoadScene(location);
        }
    }

    public void checkDenied()
    {
        checkScreenHolder.SetActive(false);
    }

    //Fades in blur
    private IEnumerator slowBlurIn()
    {
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        //While blur is not low enough
        while (dOF.focusDistance.value > wantedBlur)
        {
            //Increment blur value
            dOF.focusDistance.value -= 0.05f;
            yield return new WaitForFixedUpdate(); //Wait
        }
        //Set blur to wanted blur
        dOF.focusDistance.value = wantedBlur;
    }

    //Fades out blur
    private IEnumerator slowBlurOut()
    {      
        //While blur is not low enough
        while (dOF.focusDistance.value < 1.8f)
        {
            //Increment blur value
            dOF.focusDistance.value += 0.05f;
            yield return new WaitForFixedUpdate(); //Wait
        }
        //Rest blur
        dOF.focusDistance.value = 1.8f;

        canvas.renderMode = RenderMode.ScreenSpaceCamera;
    }

    //Slides in ui
    private IEnumerator slideIn()
    {
        //While not at wanted position
        while (rt.anchoredPosition.x < wantedPosX)
        {
            //Move transform
            rt.anchoredPosition += new Vector2(10f, 0);
            yield return new WaitForFixedUpdate(); //Wait
        }
        //Set postion to wantedx
        rt.anchoredPosition = new Vector2(wantedPosX, rt.anchoredPosition.y);

        //Turn off time
        Time.timeScale = 0f;
    }

    //Slides out ui
    private IEnumerator slideOut()
    {
        //While not at wanted position
        while (rt.anchoredPosition.x > defaultx.x)
        {
            //Move transform
            rt.anchoredPosition -= new Vector2(16f, 0);
            yield return new WaitForFixedUpdate(); //Wait
        }
        //Set postion to wantedx
        rt.anchoredPosition = defaultx;

        //Turns off pause menu UI
        pauseMenuUI.SetActive(false);
    }
}
