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
        StartCoroutine(waiter("TitleScene"));
    }

    //Trasititons to game start scene
    public void toGame()
    {
        leaving = true;
        StartCoroutine(waiter("Level"));
    }

    //Trasititons to credits scene
    public void toCredits()
    {
        leaving = true;
        StartCoroutine(waiter("CreditsScene"));
    }

    public IEnumerator waiter(string location)
    {
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(location);
    }
}
