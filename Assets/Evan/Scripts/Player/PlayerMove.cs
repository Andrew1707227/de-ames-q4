using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Editable values
    [SerializeField] float speed = 1.25f; //Player speed
    [SerializeField] float maxSpeed = 2.5f; //Player max speed
    [SerializeField] float turnSpeedUp = 3; //Speed player turns around at

    //Holds horizontal and vertical axis when using them
    float horizAxis;
    float vertAxis;

    //References
    Rigidbody2D rb2;

    // Start is called before the first frame update
    void Start()
    {
        rb2 = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Gets both axes
        horizAxis = Input.GetAxis("Horizontal");
        vertAxis = Input.GetAxis("Vertical");

        //If turning around Horizonatally
        if (rb2.velocity.x > 0 && horizAxis > 0)
        {
            horizAxis = horizAxis * turnSpeedUp;
        }
        else if (rb2.velocity.x < 0 && horizAxis < 0)
        {
            horizAxis = horizAxis * turnSpeedUp;
        }

        //Apply horizonatal movement
        rb2.velocity += new Vector2((horizAxis * Time.deltaTime) * speed, 0);


        //Apply vertical movement
        rb2.velocity += new Vector2(0, (vertAxis * Time.deltaTime) * speed);


        //Clamp to max speed
        rb2.velocity = new Vector2(Mathf.Clamp(rb2.velocity.x, -maxSpeed, maxSpeed),
                                   Mathf.Clamp(rb2.velocity.y, -maxSpeed, maxSpeed));

        //Orignial way
        /*
        //Gets both axes
        horizAxis = Input.GetAxis("Horizontal");
        vertAxis = Input.GetAxis("Vertical");


        //If turning around Horizonatally
        if (rb2.velocity.x > 0 && horizAxis < 0)
        {
            horizAxis = horizAxis * turnSpeedUp;
        }
        if (rb2.velocity.x < 0 && horizAxis > 0)
        {
            horizAxis = horizAxis * turnSpeedUp;
        }

        //Apply horizonatal movement
        rb2.AddForce(new Vector2((horizAxis * Time.deltaTime) * speed, 0), ForceMode2D.Impulse);


        //If turning around vertically
        if (rb2.velocity.y > 0 && vertAxis < 0)
        {
            vertAxis = vertAxis * turnSpeedUp;
        }
        if (rb2.velocity.y < 0 && vertAxis > 0)
        {
            vertAxis = vertAxis * turnSpeedUp;
        }

        //Apply vertical movement
        rb2.AddForce(new Vector2(0, (vertAxis * Time.deltaTime) * speed), ForceMode2D.Impulse);


        //Clamp to max speed
        rb2.velocity = new Vector2(Mathf.Clamp(rb2.velocity.x, -maxSpeed, maxSpeed),
                                   Mathf.Clamp(rb2.velocity.y, -maxSpeed, maxSpeed));
        */
    }
}
