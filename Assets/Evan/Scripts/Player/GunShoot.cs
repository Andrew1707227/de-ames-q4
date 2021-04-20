using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public GameObject bullet; //Bullet
    public GameObject cloneHolder; //Parent for bullet clone

    public AudioClip shootSFX;
    public AudioClip reloadSFX;
    public AudioClip reloadFastSFX;

    private AudioSource Asource;

    public float bulletSpeed = 25; //Holds speed of the bullet
    public float fireSpeed= 0.25f; //Holds how fast the player can shoot in seconds
    public float maxAmmo = 5f; //Holds max ammo
    public float currentAmmo = 5f; //Holds currentAmmo
    public float rechargeRate = 1f;


    float fireTimer = 0; //Times out shots
    bool reloadingFast = false; //Holds if reloading fast
    bool reloadingSlow = false; //Holds if reloading slow
    Vector2 lookDir; //Holds where the gun is looking

    //Holds angle data
    float shootAngle;
    Quaternion shootQuaternion;

    PlayerAim pa;

    // Start is called before the first frame update
    void Start()
    {
        pa = gameObject.GetComponent<PlayerAim>();
        Asource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //If trying to reload and is possible
        if (currentAmmo != maxAmmo && Input.GetKeyDown("r"))
        {

            //Check if ammo = 0 or not
            if (currentAmmo != 0)
            {
                //Set fast reload
                reloadingFast = true;
                Asource.PlayOneShot(reloadFastSFX);
            }
            else
            {
                //Set slow reload
                reloadingSlow = true;
                Asource.PlayOneShot(reloadSFX);
            }
        }

        //Checks if fast or slow reload
        if (reloadingFast)
        {
            //Increments up by rechargeRate
            currentAmmo += rechargeRate * Time.deltaTime;
        }
        else if (reloadingSlow)
        {
            //Increments up by rechargeRate / 1.1
            currentAmmo += (rechargeRate / 1.1f) * Time.deltaTime;
        }

        //Makes sure ammo doesnt get to big
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);

        //Checks if reloading is done
        if (currentAmmo == maxAmmo)
        {
            //Resets reloading bools
            reloadingSlow = false;
            reloadingFast = false;
        }

        //If not reloading, have enough ammo, not shooting to fast, and clicked mouse
        if (!reloadingFast && !reloadingSlow && currentAmmo >= 1 && Input.GetKeyDown("mouse 0") && fireTimer <= 0)
        {
            Asource.PlayOneShot(shootSFX);
            //increment ammo
            currentAmmo--;

            //Gets look diection
            lookDir = pa.currentLookDir.normalized;

            //Turns the vector into an angle
            shootAngle = (Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg);

            //Turns angle into a quaternion around the z axis
            shootQuaternion = Quaternion.AngleAxis(shootAngle, Vector3.forward);

            //Create bullet clone
            GameObject bulletClone = Instantiate(bullet, gameObject.transform.position, shootQuaternion, cloneHolder.transform);

            //Gets clone's rigidbody and give it force
            Rigidbody2D cloneRb2 = bulletClone.GetComponent<Rigidbody2D>();
            cloneRb2.AddRelativeForce(new Vector2(0, -bulletSpeed), ForceMode2D.Impulse);

            //Destroy bullet after 4 seconds
            Destroy(bulletClone, 2);

            //Reset fire timer
            fireTimer = fireSpeed;
        }
        else if (fireTimer >= 0) //If fire timer is not 0 or smaller
        {
            //Increment fire timer
            fireTimer -= Time.deltaTime;
        }
    }
}
