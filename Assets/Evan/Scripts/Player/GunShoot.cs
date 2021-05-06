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
    public bool reloadingFast = false; //Holds if reloading fast
    public bool reloadingSlow = false; //Holds if reloading slow
    float reloadTimer = 0;
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
        if (currentAmmo != maxAmmo && Input.GetKeyDown("r") && !PauseMenu.gameIsPaused)
        {

            //Check if ammo = 0 or not
            if (currentAmmo != 0)
            {
                //Set fast reload
                reloadingFast = true;

                //Plays normal reload sound
                Asource.PlayOneShot(reloadFastSFX, 0.75f);
            }
            else
            {
                //Set slow reload
                reloadingSlow = true;

                //Plays clang reload sound
                Asource.PlayOneShot(reloadSFX, 0.6f);
            }
        }

        //Checks if fast or slow reload
        if (reloadingFast)
        {
            //Increments up by rechargeRate
            reloadTimer -= rechargeRate * Time.deltaTime;
        }
        else if (reloadingSlow)
        {
            //Increments up by rechargeRate / 1.1
            reloadTimer -= (rechargeRate / 2f) * Time.deltaTime;
        }


        //Checks if reloading is done
        if (reloadTimer <= 0)
        {
            //Resets reloading bools
            reloadingSlow = false;
            reloadingFast = false;
            currentAmmo = maxAmmo;
            reloadTimer = rechargeRate;
        }

        //If not reloading, have enough ammo, not shooting to fast, and clicked mouse
        if (!reloadingFast && !reloadingSlow && currentAmmo >= 1 && Input.GetKeyDown("mouse 0") && fireTimer <= 0 && !PauseMenu.gameIsPaused)
        {
            //Plays shoot sound
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
