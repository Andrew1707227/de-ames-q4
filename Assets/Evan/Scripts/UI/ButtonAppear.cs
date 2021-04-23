using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonAppear : MonoBehaviour
{
    //Hold the object that has the scenechanger attached
    public GameObject sceneChangerObject;

    public int moveLength = 120;
    public int moveSpeed = 13;
    public int expandSpeed = 40;

    Vector2 bSize; //Holds button size
    Vector2 bPos; //Holds button position
    bool done = false; //Holds if button is done so it doesnt try to close twice

    //Componenet references
    Image i;
    RectTransform rt;
    SceneChanger sC;

    private void Awake()
    {
        rt = gameObject.GetComponent<RectTransform>();
        i = gameObject.GetComponent<Image>();
        sC = sceneChangerObject.GetComponent<SceneChanger>();

        bSize = rt.sizeDelta; //Get defualt size
        bPos = rt.anchoredPosition; //Get defualt position
        i.enabled = false; //Make button invisible
    }

    private void Start()
    {
        //Start open button
        StartCoroutine(openButton());
    }

    private void Update()
    {
        //If leaving scene and havent done this before
        if (!done && sC.leaving)
        {
            done = true; //Set done to true
            moveSpeed = moveSpeed + (moveSpeed/2);
            StartCoroutine(closeButton()); //Start close button
        }
    }

    private IEnumerator closeButton()
    {
        rt.sizeDelta = bSize; //Sets size to defualt size
        rt.anchoredPosition = bPos; //Sets position to defualt position

        //Shrink horizontally
        for (int i = (int)bSize.x; i >= 5; i -= expandSpeed)
        {
            rt.sizeDelta = new Vector2(i, bSize.y);
            yield return new WaitForFixedUpdate(); //Wait
        }

        //Set x length to five
        rt.sizeDelta = new Vector2(5, bSize.y);
        yield return new WaitForSeconds(.2f); //Wait

        //Slide off screen
        for (int i = (int)bPos.y; i >= bPos.y - moveLength; i -= moveSpeed)
        {
            rt.anchoredPosition = new Vector2(bPos.x, i);
            yield return new WaitForFixedUpdate(); //Wait
        }

        //Set position to off screen
        rt.anchoredPosition = new Vector2(bPos.x, bPos.y - moveLength);
        i.enabled = false; //Make button invisible
    }

    public IEnumerator openButton()
    {
        //Make button visible
        i.enabled = true;


        rt.sizeDelta = new Vector2(5, bSize.y); //Set x length to five
        rt.anchoredPosition = new Vector2(bPos.x, bPos.y - moveLength); //Set position to off screen


        //Slide on screen
        for (int i = (int)(bPos.y - moveLength); i <= bPos.y; i += moveSpeed)
        {
            rt.anchoredPosition = new Vector2(bPos.x, i);
            yield return new WaitForFixedUpdate(); //Wait
        }

        //Set position to defualt position
        rt.anchoredPosition = bPos;
        yield return new WaitForSeconds(.2f); //Wait

        //Expand horizontally
        for (int i = 5; i <= bSize.x; i += expandSpeed)
        {
            rt.sizeDelta = new Vector2(i, bSize.y);
            yield return new WaitForFixedUpdate(); //Wait
        }

        //Set size to defualt size
        rt.sizeDelta = bSize;
    }

}
