using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsAppear : MonoBehaviour
{
    public int moveLength = 120;
    public int moveSpeed = 13;
    public int expandSpeed = 40;

    Vector2 bSize; //Holds button size
    Vector2 bPos; //Holds button position
    bool done = false; //Holds if button is done so it doesnt try to close twice

    //Componenet references
    Image i;
    RectTransform rt;

    private void Awake() {
        rt = gameObject.GetComponent<RectTransform>();
        i = gameObject.GetComponent<Image>();

        bSize = rt.sizeDelta; //Get defualt size
        bPos = rt.anchoredPosition; //Get defualt position
        i.enabled = false; //Make button invisible
    }

    private void Start() {
        //Start open button
        StartCoroutine(openButton());
    }

    public IEnumerator openButton() {
        //Make button visible
        i.enabled = true;


        rt.sizeDelta = new Vector2(5, bSize.y); //Set x length to five
        rt.anchoredPosition = new Vector2(bPos.x, bPos.y - moveLength); //Set position to off screen


        //Slide on screen
        for (int i = (int)(bPos.y - moveLength); i <= bPos.y; i += moveSpeed) {
            rt.anchoredPosition = new Vector2(bPos.x, i);
            yield return new WaitForFixedUpdate(); //Wait
        }

        //Set position to defualt position
        rt.anchoredPosition = bPos;
        yield return new WaitForSeconds(.2f); //Wait

        //Expand horizontally
        for (int i = 5; i <= bSize.x; i += expandSpeed) {
            rt.sizeDelta = new Vector2(i, bSize.y);
            yield return new WaitForFixedUpdate(); //Wait
        }

        //Set size to defualt size
        rt.sizeDelta = bSize;
    }

}
