using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonAppear : MonoBehaviour
{
    public GameObject sceneChangerObject;

    Vector2 bSize;
    Vector2 bPos;
    bool done = false;

    Image i;
    RectTransform rt;
    SceneChanger sC;

    private void Awake()
    {
        rt = gameObject.GetComponent<RectTransform>();
        i = gameObject.GetComponent<Image>();
        sC = sceneChangerObject.GetComponent<SceneChanger>();

        bSize = rt.sizeDelta;
        bPos = rt.anchoredPosition;
        i.enabled = false;
    }

    private void Start()
    {
        StartCoroutine(openButton());
    }

    private void Update()
    {
        if (!done && sC.leaving)
        {
            done = true;
            StartCoroutine(closeButton());
        }
    }

    private IEnumerator closeButton()
    {
        rt.sizeDelta = bSize;
        rt.anchoredPosition = bPos;

        for (int i = (int)bSize.x; i >= 5; i -= 40)
        {
            rt.sizeDelta = new Vector2(i, bSize.y);
            yield return new WaitForFixedUpdate();
        }
        rt.sizeDelta = new Vector2(5, bSize.y);
        yield return new WaitForSeconds(.2f);

        for (int i = (int)bPos.y; i >= bPos.y - 100; i -= 12)
        {
            rt.anchoredPosition = new Vector2(bPos.x, i);
            yield return new WaitForFixedUpdate();
        }
        rt.anchoredPosition = new Vector2(bPos.x, bPos.y - 100);
        i.enabled = false;
    }

    public IEnumerator openButton()
    {
        i.enabled = true;
        rt.sizeDelta = new Vector2(5, bSize.y);
        rt.anchoredPosition = new Vector2(bPos.x, bPos.y - 100);

        for (int i = (int)(bPos.y - 100); i <= bPos.y; i += 12)
        {
            rt.anchoredPosition = new Vector2(bPos.x, i);
            yield return new WaitForFixedUpdate();
        }
        rt.anchoredPosition = bPos;
        yield return new WaitForSeconds(.2f);

        for (int i = 5; i <= bSize.x; i += 40)
        {
            rt.sizeDelta = new Vector2(i, bSize.y);
            yield return new WaitForFixedUpdate();
        }
        rt.sizeDelta = bSize;
    }

}
