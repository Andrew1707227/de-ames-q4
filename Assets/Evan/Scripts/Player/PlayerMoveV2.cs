using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveV2 : MonoBehaviour
{
    //Editable values
    [SerializeField] float speed = 1.25f; //Player speed
    [SerializeField] float maxSpeed = 2.5f; //Player max speed
    public float burstLength = 0.25f; //Holds how long the burst can take maximum

    //Holds horizontal and vertical axis when using them
    float horizAxis;
    float vertAxis;

    public float horizTimer; //Holds current boost time for horizontal axis
    public float vertTimer; //Holds current boost time for vertical axis
    float diagonalTimer; //Holds current boost time for when going diagonally

    //References
    Rigidbody2D rb2;

    // Start is called before the first frame update
    void Start()
    {
        rb2 = gameObject.GetComponent<Rigidbody2D>();

        //Sets up defaults for timers
        horizTimer = burstLength;
        vertTimer = burstLength;
        diagonalTimer = burstLength;
    }

    // Update is called once per frame
    void Update()
    {
        //Gets both axes
        horizAxis = Input.GetAxis("Horizontal");
        vertAxis = Input.GetAxis("Vertical");

        //Checks if going diagonally
        if (horizAxis != 0 && vertAxis != 0)
        {
            //Checks if diagaonal timer is complete
            if (diagonalTimer > 0)
            {
                //Adds force
                rb2.AddForce(new Vector2((horizAxis * speed) * (burstLength / diagonalTimer), (vertAxis * speed) * (burstLength / diagonalTimer)), ForceMode2D.Impulse);

                //Increments timers
                horizTimer -= Time.deltaTime;
                vertTimer -=Time.deltaTime;
                diagonalTimer -= Time.deltaTime;
            }
            else
            {
                //Resets timer
                diagonalTimer = burstLength;
            }
        }
        else
        {
            //Resets diagonal timer
            diagonalTimer = burstLength;

            //Checks if moving horizontally
            if (horizAxis != 0)
            {
                //Checks if horizontal timer is complete
                if (horizTimer > 0)
                {
                    //Adds force
                    rb2.AddForce(new Vector2((horizAxis * speed) * (burstLength / horizTimer), 0), ForceMode2D.Impulse);

                    //Increments timer
                    horizTimer -= Time.deltaTime;
                }
            }
            else
            {
                //Resets timer
                horizTimer = burstLength;
            }

            //Checks if moving verticly
            if (vertAxis != 0)
            {
                //Checks if vertical timer is complete
                if (vertTimer > 0)
                {
                    //Adds force
                    rb2.AddForce(new Vector2(0, (vertAxis * speed) * (burstLength / vertTimer)), ForceMode2D.Impulse);

                    //Increments timer
                    horizTimer -= Time.deltaTime;
                }
            }
            else
            {
                //Resets timer
                vertTimer = burstLength;
            }
        }

        //Clamp to max speed
        rb2.velocity = new Vector2(Mathf.Clamp(rb2.velocity.x, -maxSpeed, maxSpeed),
                                   Mathf.Clamp(rb2.velocity.y, -maxSpeed, maxSpeed));
    }
}
