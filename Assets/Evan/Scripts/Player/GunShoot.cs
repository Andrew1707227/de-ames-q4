using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public GameObject bullet; //Bullet
    public GameObject cloneHolder; //Parent for bullet clone

    //Holds speed of the bullet
    public float bulletSpeed = 25;
    //Holds how fast the player can shoot in seconds
    public float fireSpeed= 0.25f;

    //Times out shots
    float fireTimer = 0;

    //Holds where the gun is looking
    Vector2 lookDir;

    //Holds angle data
    float shootAngle;
    Quaternion shootQuaternion;

    PlayerAim pa;

    // Start is called before the first frame update
    void Start()
    {
        pa = gameObject.GetComponent<PlayerAim>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && fireTimer <= 0)
        {
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

            fireTimer = fireSpeed;
        }
        else if (fireTimer >= 0)
        {
            fireTimer -= Time.deltaTime;
        }
    }
}
