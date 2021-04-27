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

    public void toGameCutScene()
    {
        sC.leaving = true;
        StartCoroutine(waiter());
    }

    //Waits for waitTime
    public IEnumerator waiter()
    {
        yield return new WaitForSeconds(1.3f);

        while (i.color.a > 0)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - 0.005f);
            yield return new WaitForSeconds(0.01f);
        }

        a.enabled = true;

        yield return new WaitForSeconds(15);
        SceneManager.LoadScene("TODDIntro");
    }
}
