using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialBarFill : MonoBehaviour
{
    //Holds reference to player
    public GameObject player;

    //Holds current precent of Pops
    public float curPopPrecent;

    float minValue = 0.25f;
    public float maxValue = 0.75f;

    float currentPopCount; //Holds the current ammo count
    float maxPops; //Holds max ammo
    Color baseColor; //Holds original color

    PlayerPop p;
    public Image i;
        
    // Start is called before the first frame update
    void Start()
    {
        p = player.GetComponent<PlayerPop>();
        i = gameObject.GetComponent<Image>();

        //Gets defualt values
        currentPopCount = p.currentPops;
        maxPops = p.maxPops;
        baseColor = i.color;

        i.fillAmount = maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        //Gets current ammo count
        currentPopCount = p.currentPops;

        //Gets current ammo precent
        curPopPrecent = currentPopCount / maxPops;

        //If slider is higher than ammo
        if (i.fillAmount > curPopPrecent * (maxValue - minValue) + minValue)
        {
            //Slide value down in tell it catches up to ammo
            i.fillAmount += (((curPopPrecent * (maxValue - minValue) + minValue) - i.fillAmount)) * Time.deltaTime;
        }

        //Uncomment this if it is ever needed to refill the dial (Not currently needed but not positive)
        //else
        //{
        //    //Set slider value to current ammo count
        //    i.fillAmount = curPopPrecent * (maxValue - minValue) + minValue;
        //}

        //Makes sure the bar doesnt get to big or small
        i.fillAmount = Mathf.Clamp(i.fillAmount, minValue, maxValue);

        //Get current slider position in a percentage
        float changePercent = 1 - i.fillAmount - 0.25f;

        //Use precent to make color darker as bar gets lower
        i.color = new Color(baseColor.r - (baseColor.r * changePercent),
                            baseColor.g - (baseColor.g * changePercent),
                            baseColor.b - (baseColor.b * changePercent));
    }
}
