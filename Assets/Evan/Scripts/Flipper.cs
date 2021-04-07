using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    //Gets robot
    public GameObject robot;
    RobotFollow rf;
    RobotFollowV2 rf2;

    //Reference to main Camera
    public Camera cam;

    //Holds parentm robot and own spriteRenderer referencees
    SpriteRenderer pSR;
    SpriteRenderer rSR;
    SpriteRenderer sR;

    //Holds mouse position
    Vector2 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        pSR = gameObject.transform.parent.GetComponent<SpriteRenderer>();
        rSR = robot.GetComponent<SpriteRenderer>();
        sR = gameObject.GetComponent<SpriteRenderer>();
        rf = robot.GetComponent<RobotFollow>();
        rf2 = robot.GetComponent<RobotFollowV2>();

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
            rSR.flipX = true;
            sR.flipX = true;

            //Flip robot follow position
            rf.offset = new Vector3(-rf.baseOffset.x, rf.baseOffset.y, 0);
            rf.currentOffset = new Vector3(-rf.baseOffset.x, rf.baseOffset.y, 0);
            rf2.offset = new Vector3(-rf2.baseOffset.x, rf2.baseOffset.y, 0);
            rf2.currentOffset = new Vector3(-rf2.baseOffset.x, rf2.baseOffset.y, 0);
        }
        if (mousePos.x < transform.position.x)
        {
            pSR.flipX = false;
            rSR.flipX = false;
            sR.flipX = false;

            //Flip robot follow position
            rf.offset = new Vector3(rf.baseOffset.x, rf.baseOffset.y, 0);
            rf.currentOffset = new Vector3(rf.baseOffset.x, rf.baseOffset.y, 0);
            rf2.offset = new Vector3(rf2.baseOffset.x, rf2.baseOffset.y, 0);
            rf2.currentOffset = new Vector3(rf2.baseOffset.x, rf2.baseOffset.y, 0);
        }
    }
}
