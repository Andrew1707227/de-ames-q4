using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderFollow : MonoBehaviour
{

    public GameObject player;
    public GameObject targetHolder;
    public GameObject spiderHead;
    public GameObject spiderHeadRight;
    public GameObject damageBox;
    public GameObject damageBoxRight;
    public GameObject leftLegs;
    public GameObject rightLegs;
    public GameObject leftTargets;
    public GameObject rightTargets;
    public GameObject[] legs = new GameObject[12];
    public GameObject[] targets = new GameObject[12];
    public GameObject[] feet = new GameObject[12];

    public float speed = 2;

    [HideInInspector]
    public bool facingLeft = true;

    SpriteRenderer sr;
    Rigidbody2D rb2;
    BodyHover bh;
    SpiderSpinner ss;
    Transform t;
    Transform pT;
    Transform[] tTS = new Transform[12];
    Transform[] tFS = new Transform[12];
    IKSolver[] iKS = new IKSolver[12];
    PolygonCollider2D[] pc = new PolygonCollider2D[2];

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        bh = gameObject.GetComponent<BodyHover>();
        ss = gameObject.GetComponent<SpiderSpinner>();
        t = gameObject.GetComponent<Transform>();
        pT = player.GetComponent<Transform>();

        for (int i = 0; i <= 11; i++)
        {
            tTS[i] = targets[i].GetComponent<Transform>();
        }
        for (int i = 0; i <= 11; i++)
        {
            tFS[i] = feet[i].GetComponent<Transform>();
        }
        for (int i = 0; i <= 11; i++)
        {
            iKS[i] = legs[i].GetComponent<IKSolver>();
        }

        pc = gameObject.GetComponents<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 localPpos = t.InverseTransformPoint(pT.position);

        if (localPpos.y > 7)
        {
            if (!facingLeft)
            {
                facingLeft = true;
                spiderFlip();
            }
            rb2.velocity = -transform.right * speed;
        }
        else if (localPpos.x < -0.5f)
        {
            if (!facingLeft)
            {
                facingLeft = true;
                spiderFlip();
            }
            rb2.velocity = -transform.right * speed;
        }
        else if(localPpos.x > 0.5f)
        {
            if (facingLeft)
            {
                facingLeft = false;
                spiderFlip();
            }
            rb2.velocity = transform.right * speed;
        }
    }

    private void spiderFlip()
    {
        //Turns off ik
        for (int i = 0; i <= 11; i++)
        {
            iKS[i].enabled = false;
        }

        leftTargets.SetActive(!leftTargets.activeSelf);
        leftLegs.SetActive(!leftLegs.activeSelf);
        spiderHead.SetActive(!spiderHead.activeSelf);
        damageBox.SetActive(!damageBox.activeSelf);
        pc[0].enabled = !pc[0].enabled;

        rightTargets.SetActive(!rightTargets.activeSelf);
        rightLegs.SetActive(!rightLegs.activeSelf);
        spiderHeadRight.SetActive(!spiderHeadRight.activeSelf);
        damageBoxRight.SetActive(!damageBoxRight.activeSelf);
        pc[1].enabled = !pc[1].enabled;

        sr.flipX = !sr.flipX;

        //Sets target pos to new foot pos
        for (int i = 0; i <= 11; i++)
        {
            tTS[i].position = tFS[i].position;
        }

        /*
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
        */

        //Turns on ik
        for (int i = 0; i <= 11; i++)
        {
            iKS[i].enabled = true;
        }
    }
}
