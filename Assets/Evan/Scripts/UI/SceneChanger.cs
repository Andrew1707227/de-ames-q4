using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public bool leaving;

    //Trasititons to title scene
    public void toTitle()
    {
        leaving = true;
        StartCoroutine(waiter("TitleScene", 0.5f));
    }

    //Trasititons to game start scene
    public void toGame()
    {
        leaving = true;
        StartCoroutine(waiter("Level", 1.3f));
    }

    //Trasititons to credits scene
    public void toCredits()
    {
        leaving = true;
        StartCoroutine(waiter("CreditsScene", 1.3f));
    }

    //Exit game
    public void exit()
    {
        Debug.Log("Would of quit if built");
        Application.Quit();
    }

    //Waits for waitTime
    public IEnumerator waiter(string location, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(location);
    }
}
