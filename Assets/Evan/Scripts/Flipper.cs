using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    //Gets robot
    public GameObject robot;
    RobotFollow rf;

    //Gets all pops
    public GameObject popHolder;

    //Reference to main Camera
    public Camera cam;

    //Tells pops to flip their own velocity
    public bool Popflipx = false;

    //Holds parentm robot and own spriteRenderer referencees
    SpriteRenderer pSR;
    SpriteRenderer rSR;
    SpriteRenderer sR;

    Transform pT;

    //Holds mouse position
    Vector2 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        pSR = gameObject.transform.parent.GetComponent<SpriteRenderer>();
        rSR = robot.GetComponent<SpriteRenderer>();
        sR = gameObject.GetComponent<SpriteRenderer>();
        rf = robot.GetComponent<RobotFollow>();
        pT = popHolder.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Sets mousePos to current mouse position
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //Flip player based on aiming direction
        if (mousePos.x > transform.position.x)
        {
            pSR.flipX = true;
            sR.flipX = true;

            if (rf.enabled)
            {
                rSR.flipX = true;
            }

            //Inform unknown number of pops to flip their own velocity
            Popflipx = true;

            //Flip pops
            pT.localScale = new Vector3(-1, 1, 1);

            //Flip robot follow position
            rf.offset = new Vector3(-rf.baseOffset.x, rf.baseOffset.y, 0);
            rf.currentOffset = new Vector3(-rf.baseOffset.x, rf.baseOffset.y, 0);
        }
        if (mousePos.x < transform.position.x)
        {
            pSR.flipX = false;
            sR.flipX = false;

            if (rf.enabled)
            {
                rSR.flipX = false;
            }

                //Inform unknown number of pops to flip their own velocity
                Popflipx = false;

            //Flip pops
            pT.localScale = new Vector3(1, 1, 1);

            //Flip robot follow position
            rf.offset = new Vector3(rf.baseOffset.x, rf.baseOffset.y, 0);
            rf.currentOffset = new Vector3(rf.baseOffset.x, rf.baseOffset.y, 0);
        }
    }
}
