using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopPush : MonoBehaviour
{
    //Holds player and arm names to find them as a prefab
    public string playerName;
    public string armName;

    //Holds direction to push player
    public Vector3 pushDirection;

    Vector3 originalPD;

    bool startFlip;
    bool lastFlip;

    Rigidbody2D pRB2;
    Flipper aF;
    ParticleSystem cPS;

    // Start is called before the first frame update
    void Start()
    {
        pRB2 = GameObject.Find(playerName).GetComponent<Rigidbody2D>();
        aF = GameObject.Find(armName).GetComponent<Flipper>();
        cPS = GetComponentInChildren<ParticleSystem>();

        //Get orginalFlip direction
        originalPD = pushDirection;

        //Check starting direction
        if (transform.rotation.z == -90)
        {
            //Set up startFlip correctly so it rotates at the right time
            startFlip = true;
        }
        else
        {
            //Set up startFlip correctly so it rotates at the right time
            startFlip = false;
        }

        //Set lastflip to the current flipping bool
        lastFlip = aF.Popflipx;

    }

    // Update is called once per frame
    void Update()
    {
        //Pushes player
        pRB2.AddForce(pushDirection/2 * Time.deltaTime, ForceMode2D.Impulse);

        //Check if the flip change from last time
        if (lastFlip != aF.Popflipx)
        {
            //Flip start flip
            startFlip = !startFlip;

            //Get new flip direction
            lastFlip = aF.Popflipx;
        }

        //If startbool is true
        if (startFlip == true)
        {
            //Flip x in push direction
            pushDirection = new Vector3(-originalPD.x ,pushDirection.y , pushDirection.z);
        }
        else
        {
            //Flip x in push direction
            pushDirection = new Vector3(originalPD.x, pushDirection.y, pushDirection.z);
        }

        //If bool from flipper says to flip
        if (aF.Popflipx)
        {
            //Preform black magic I found on the internet to change the shape module and tunr the particles around
            var newShape = cPS.shape;
            newShape.rotation = new Vector3(newShape.rotation.x, Mathf.Abs(newShape.rotation.y), newShape.rotation.z);
        }
        else
        {
            //Preform black magic I found on the internet to change the shape module and tunr the particles around
            var newShape = cPS.shape;
            newShape.rotation = new Vector3(newShape.rotation.x, -Mathf.Abs(newShape.rotation.y), newShape.rotation.z);
        }
    }
}
