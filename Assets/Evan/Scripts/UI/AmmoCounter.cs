using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
    //Holds reference to arm
    public GameObject arm;

    float currentAmmoCount; //Holds the current ammo count


    GunShoot gs;
    Text t;

    // Start is called before the first frame update
    void Start()
    {
        gs = arm.GetComponent<GunShoot>();
        t = gameObject.GetComponent<Text>();

        //Gets defualt values
        currentAmmoCount = gs.currentAmmo;

        //Sets text to defualt text value
        t.text = "" + (int)currentAmmoCount;
    }

    // Update is called once per frame
    void Update()
    {       
        //Gets current ammo count
        currentAmmoCount = gs.currentAmmo;

        //Updates text to match current ammo
        t.text = "" + (int)currentAmmoCount;

    }
}
