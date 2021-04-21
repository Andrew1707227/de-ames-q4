using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
    //Holds reference to arm
    public GameObject arm;

    float currentAmmoCount; //Holds the current ammo count

    string loading = ".  ";
    bool stop = false;

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

        if ((gs.reloadingFast || gs.reloadingSlow) && !stop)
        {
            stop = true;
            StartCoroutine(loadingDots());
        }
        else if(!gs.reloadingFast && !gs.reloadingSlow)
        {
            stop = false;

            //Updates text to match current ammo
            t.text = (int)currentAmmoCount + "/5";
        }
    }

    public IEnumerator loadingDots()
    {
        while (gs.reloadingFast || gs.reloadingSlow)
        {
            t.text = loading;

            if (loading == ".  ")
            {
                loading = ".. ";
            }
            else if (loading == ".. ")
            {
                loading = "...";
            }
            else
            {
                loading = ".  ";
            }

            yield return new WaitForSeconds(.2f); //Wait
        }

        loading = ".  ";
    }
}
