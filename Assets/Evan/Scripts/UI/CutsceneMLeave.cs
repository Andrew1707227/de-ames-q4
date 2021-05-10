using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneMLeave : MonoBehaviour
{
    //Hold the object that has the scenechanger attached
    public GameObject sceneChangerObject;

    public GameObject backGround;
    public GameObject cutScene;

    SceneChanger sC;
    Image i;
    Animator a;

    // Start is called before the first frame update
    void Start()
    {
        sC = sceneChangerObject.GetComponent<SceneChanger>();
        i = backGround.GetComponent<Image>();
        a = cutScene.GetComponent<Animator>();
    }

    //Starts cutscene
    public void toGameCutScene()
    {
        //Inform buttons to leave
        sC.leaving = true;

        //Starts cutscene
        StartCoroutine(waiter());
    }

    //Waits for waitTime
    public IEnumerator waiter()
    {
        yield return new WaitForSeconds(1.3f); //Wait

        while (i.color.a > 0)
        {
            //Fade off background
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - 0.01f);
            yield return new WaitForFixedUpdate();//Wait
        }

        //Turn on cutscene
        a.enabled = true;

        yield return new WaitForSeconds(15);//wait

        //Start game
        SceneManager.LoadScene("TODDIntro");
    }
}
