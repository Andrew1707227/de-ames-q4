using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBarScale : MonoBehaviour
{
    //Holds reference to arm
    public GameObject arm;

    float currentAmmoCount; //Holds the current ammo count
    float maxAmmo; //Holds max ammo

    Slider s;
    GunShoot gs;

    // Start is called before the first frame update
    void Start()
    {
        gs = arm.GetComponent<GunShoot>();
        s = gameObject.GetComponent<Slider>();

        //Gets defualt values
        currentAmmoCount = gs.currentAmmo;
        maxAmmo = gs.maxAmmo;

        //Sets defualt values in slider
        s.maxValue = maxAmmo;
        s.value = currentAmmoCount;
    }

    // Update is called once per frame
    void Update()
    {
        //Gets current ammo count
        currentAmmoCount = gs.currentAmmo;

        //Set slider value to current ammo count
        s.value = currentAmmoCount;
    }
}
