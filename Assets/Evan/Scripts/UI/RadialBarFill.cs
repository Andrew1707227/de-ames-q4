using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialBarFill : MonoBehaviour
{
    //Holds reference to arm and fill
    public GameObject arm;

    //Holds current precent of ammo
    public float curAmmoPrecent;

    float minValue = 0.25f;
    float maxValue = 0.75f;

    float currentAmmoCount; //Holds the current ammo count
    float maxAmmo; //Holds max ammo
    Color baseColor; //Holds original color

    GunShoot gs;
    Image i;

    // Start is called before the first frame update
    void Start()
    {
        gs = arm.GetComponent<GunShoot>();
        i = gameObject.GetComponent<Image>();

        //Gets defualt values
        currentAmmoCount = gs.currentAmmo;
        maxAmmo = gs.maxAmmo;
        baseColor = i.color;

        i.fillAmount = 0.75f;
    }

    // Update is called once per frame
    void Update()
    {
        //Gets current ammo count
        currentAmmoCount = gs.currentAmmo;

        //Gets current ammo precent
        curAmmoPrecent = currentAmmoCount / maxAmmo;

        //If slider is higher than ammo
        if (i.fillAmount > curAmmoPrecent * (maxValue - minValue) + minValue)
        {
            //Slide value down in tell it catches up to ammo
            i.fillAmount -= 1f * Time.deltaTime;
        }
        else
        {
            //Set slider value to current ammo count
            i.fillAmount = curAmmoPrecent * (maxValue - minValue) + minValue;
        }

        //Makes sure the bar doesnt get to big or small
        i.fillAmount = Mathf.Clamp(i.fillAmount, minValue, maxValue);

        //Get current slider position in a percentage
        float changePercent = 1 - (curAmmoPrecent * (maxValue - minValue) + minValue);

        //Use precent to make color darker as bar gets lower
        i.color = new Color(baseColor.r - ((baseColor.r * changePercent) / 3),
                            baseColor.g - ((baseColor.g * changePercent) / 3),
                            baseColor.b - ((baseColor.b * changePercent)) / 3);
    }
}
