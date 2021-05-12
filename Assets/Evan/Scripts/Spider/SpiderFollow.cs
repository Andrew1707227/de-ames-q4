using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderFollow : MonoBehaviour
{

    public GameObject player;
    public GameObject targetHolder;
    public GameObject[] legs = new GameObject[6];
    public GameObject[] targets = new GameObject[6];
    public bool flipped = false;

    GameObject[] feet = new GameObject[6];
    bool facingLeft = true;

    int layerMask;

    Rigidbody2D rb2;
    Transform t;
    Transform pT;
    Transform[] tLS = new Transform[6];
    Transform[] tTS = new Transform[6];
    Transform[] tFS = new Transform[6];
    TargetMove[] tM= new TargetMove[6];
    IKSolver[] iKS = new IKSolver[6];

    // Start is called before the first frame update
    void Start()
    {
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        t = gameObject.GetComponent<Transform>();
        pT = player.GetComponent<Transform>();

        //Get layermask
        layerMask = ~LayerMask.GetMask("Spider");

        for (int i = 0; i <= 5; i++)
        {
            tLS[i] = legs[i].GetComponent<Transform>();
        }
        for (int i = 0; i <= 5; i++)
        {
            tTS[i] = targets[i].GetComponent<Transform>();
        }
        for (int i = 0; i <= 5; i++)
        {
            tFS[i] = legs[i].GetComponentInChildren<Transform>().GetComponentInChildren<Transform>();
        }
        for (int i = 0; i <= 5; i++)
        {
            tM[i] = targets[i].GetComponent<TargetMove>();
        }
        for (int i = 0; i <= 5; i++)
        {
            iKS[i] = legs[i].GetComponent<IKSolver>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 localPpos = t.InverseTransformPoint(pT.position);

        if (localPpos.y > 5)
        {
            rb2.velocity = -transform.right * 2;
        }
        else if (localPpos.x < -0.5f)
        {
            if (!facingLeft)
            {
                spiderFlip();
            }
            rb2.velocity = -transform.right * 2;
        }
        else if(localPpos.x > 0.5f)
        {
            if (facingLeft)
            {
                spiderFlip();
            }
            rb2.velocity = transform.right * 2;
        }
    }

    private void spiderFlip()
    {
        //Turns off ik
        for(int i = 0; i <= 5; i++)
        {
            iKS[i].enabled = false;
        }

        //Rotates spider based on current rotaion
        if (transform.rotation.y == 0)
        {
            flipped = true;
            transform.Rotate(0, 180, 0);
        }
        else
        {
            flipped = false;
            transform.Rotate(0, -180, 0);
        }

        for (int i = 0; i <= 5; i++)
        {
            //tTS[i].position = tFS[i].position;

            float curFootOffset;
            if (flipped)
            {
                curFootOffset = -tM[i].footOffset;
            }
            else
            {
                curFootOffset = tM[i].footOffset;
            }

            float curWantedX = tLS[i].position.x + curFootOffset;

            Vector2 floorCheckStart = new Vector2(curWantedX, tLS[i].position.y);
            RaycastHit2D floorChecker = Physics2D.Raycast(floorCheckStart, -t.up, 4f, layerMask);
            Debug.DrawRay(floorCheckStart, -t.up, Color.green, 3);

            Debug.Log(floorChecker.point);

            tTS[i].position = floorChecker.point;
        }

        //Turns on ik
        for (int i = 0; i <= 5; i++)
        {
            iKS[i].enabled = true;
        }
    }
}
