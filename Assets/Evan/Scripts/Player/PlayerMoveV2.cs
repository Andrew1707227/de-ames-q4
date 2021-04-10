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

    public float horizTimer;
    public float vertTimer;

    //References
    Rigidbody2D rb2;

    // Start is called before the first frame update
    void Start()
    {
        rb2 = gameObject.GetComponent<Rigidbody2D>();

        horizTimer = burstLength;
        vertTimer = burstLength;
    }

    // Update is called once per frame
    void Update()
    {
        //Gets both axes
        horizAxis = Input.GetAxis("Horizontal");
        vertAxis = Input.GetAxis("Vertical");

        if (horizAxis != 0)
        {
            if (horizTimer > 0)
            {
                rb2.AddForce(new Vector2((horizAxis * speed) * (burstLength / horizTimer), 0), ForceMode2D.Impulse);

                horizTimer -= Time.deltaTime;
            }
        }
        else
        {
            horizTimer = burstLength;
        }


        if (vertAxis != 0)
        {
            if (vertTimer > 0)
            {
                rb2.AddForce(new Vector2(0, (vertAxis * speed) * (burstLength / vertTimer)), ForceMode2D.Impulse);

                horizTimer -= Time.deltaTime;
            }
        }
        else
        {
            vertTimer = burstLength;
        }

        //Clamp to max speed
        rb2.velocity = new Vector2(Mathf.Clamp(rb2.velocity.x, -maxSpeed, maxSpeed),
                                   Mathf.Clamp(rb2.velocity.y, -maxSpeed, maxSpeed));
    }
}
