using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderFollow : MonoBehaviour
{

    public GameObject player;
    public GameObject targetHolder;
    public GameObject[] legs = new GameObject[6];
    public GameObject[] targets = new GameObject[6];
    public GameObject[] feet = new GameObject[6];
    public bool flipped = false;
    bool facingLeft = true;

    int layerMask;

    Rigidbody2D rb2;
    BodyHover bh;
    SpiderSpinner ss;
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
        bh = gameObject.GetComponent<BodyHover>();
        ss = gameObject.GetComponent<SpiderSpinner>();
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
            tFS[i] = feet[i].GetComponent<Transform>();
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
            //rb2.velocity = -transform.right * 2;
        }
        else if (localPpos.x < -0.5f)
        {
            if (!facingLeft)
            {
                StartCoroutine(spiderFlip());
            }
            //rb2.velocity = -transform.right * 2;
        }
        else if(localPpos.x > 0.5f)
        {
            if (facingLeft)
            {
                StartCoroutine(spiderFlip());
            }
            //rb2.velocity = transform.right * 2;
        }
    }

    private IEnumerator spiderFlip()
    {
        //Turns off ik
        for (int i = 0; i <= 5; i++)
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

        //Sets target pos to new foot pos
        for (int i = 0; i <= 5; i++)
        {
            tTS[i].position = tFS[i].position;
        }

        //Sets correct hover hight
        RaycastHit2D floorDistance = Physics2D.Raycast(transform.position, -transform.up, 10, layerMask);

        if (floorDistance.distance != bh.hoverDist)
        {
            Vector3 wantedPos = new Vector3(floorDistance.point.x, floorDistance.point.y, 0) + transform.up * 1.1f;

            transform.position = wantedPos;
        }

        //Sets correct rotation
        for (int i = 1; i <= 20; i++)
        {
            //Checks grond distance on both ends
            RaycastHit2D leftDistance = Physics2D.Raycast(ss.lT.position, -transform.up, 10, layerMask);
            RaycastHit2D rightDistance = Physics2D.Raycast(ss.rT.position, -transform.up, 10, layerMask);

            //If distance uneven enough rotate until fixed
            if (Mathf.Round(leftDistance.distance * 10f) / 10f > Mathf.Round(rightDistance.distance * 10f) / 10f)
            {
                transform.Rotate(new Vector3(0, 0, 0.75f));
            }
            else if (Mathf.Round(leftDistance.distance * 10f) / 10f < Mathf.Round(rightDistance.distance * 10f) / 10f)
            {
                transform.Rotate(new Vector3(0, 0, -0.75f));
            }
        }

        yield return new WaitForFixedUpdate();

        //Turns on ik
        for (int i = 0; i <= 5; i++)
        {
            //iKS[i].enabled = true;
        }
    }
}
