using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    public GameObject player;
    public GameObject acidSpit;
    public GameObject[] spitParticles = new GameObject[2];
    public GameObject[] shootPoints = new GameObject[2];
    public GameObject[] heads = new GameObject[2];
    public GameObject[] leapFrontCheck = new GameObject[4];
    public GameObject[] targets = new GameObject[12];

    public float bodySpin;
    public float headSpin;

    public float attackTime = 10;
    float curAttackTime;

    bool attacking = false;
    int choice = 0;

    //int layerMask;
    public LayerMask layerMask;

    Rigidbody2D rb2;
    SpiderSpinner ss;
    BodyHover bh;
    SpiderFollow sf;
    Transform pT;
    Transform[] spT = new Transform[2];
    Transform[] hT = new Transform[2];
    ParticleSystem[] ps = new ParticleSystem[2];
    Transform[] lfT = new Transform[4];
    LegRear[] lr = new LegRear[4];
    LegPounce[] lp = new LegPounce[12];

    // Start is called before the first frame update
    void Start()
    {
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        ss = gameObject.GetComponent<SpiderSpinner>();
        bh = gameObject.GetComponent<BodyHover>();
        sf = gameObject.GetComponent<SpiderFollow>();
        pT = player.GetComponent<Transform>();

        for (int i = 0; i <= 1; i++)
        {
            ps[i] = spitParticles[i].GetComponent<ParticleSystem>();
            spT[i] = shootPoints[i].GetComponent<Transform>();
            hT[i] = heads[i].GetComponent<Transform>();
        }
        for (int i = 0; i <= 3; i++)
        {
            lfT[i] = leapFrontCheck[i].GetComponent<Transform>();
            lr[i] = targets[i].GetComponent<LegRear>();
        }
        for (int i = 0; i <= 11; i++)
        {
            lp[i] = targets[i].GetComponent<LegPounce>();
        }

        //layerMask = ~LayerMask.GetMask("Spider");

        curAttackTime = attackTime;
    }

    // Update is called once per frame
    void Update()
    {
        curAttackTime -= Time.deltaTime;

        if (curAttackTime < attackTime / 2 && !attacking)
        {
            if (choice == 0)
            {
                choice = Random.Range(1, 4);
            }

            if (choice == 1)
            {
                Vector3 toPlayer = pT.position - transform.position;

                RaycastHit2D wallChecker;
                if (sf.facingLeft)
                {
                    wallChecker = Physics2D.Raycast(lfT[0].position, -transform.right, 9, layerMask);
                    //Debug.DrawRay(lfT[0].position, -transform.right * 9, Color.white, 2);
                }
                else
                {
                    wallChecker = Physics2D.Raycast(lfT[0].position, transform.right, 9, layerMask);
                    //Debug.DrawRay(lfT[0].position, transform.right * 9, Color.white, 2);
                }

                if (wallChecker.point != Vector2.zero && wallChecker.distance > 8 && toPlayer.magnitude < 10)
                {
                    attacking = true;
                    StartCoroutine(leap());
                }
                else
                {
                    //choice = 2;
                }
            }
            else if (choice == 2 || choice == 3)
            {
                Vector3 toPlayer = pT.position - transform.position;

                Vector3 startDir = -transform.right;
                if (!sf.facingLeft)
                {
                    startDir = -startDir;
                }

                float angleTo = Vector3.Angle(startDir, toPlayer);

                if (angleTo <= 45 && toPlayer.magnitude < 7)
                {
                    attacking = true;
                    StartCoroutine(rearUpSpit(true));
                }
            }

            if (curAttackTime <= 0 && !attacking)
            {
                attacking = true;
                StartCoroutine(rearUpSpit(true));
            }
        }
    }

    private IEnumerator leap()
    {
        choice = 0;
        rb2.velocity = Vector3.zero;

        ss.enabled = false;
        bh.enabled = false;
        sf.enabled = false;

        int times = 35;
        Vector3 endPoint;
        Vector3 bodyMove;
        Vector3 difference;
        Vector3 lift;
        if (sf.facingLeft)
        {
            endPoint = lfT[1].position;
            difference = endPoint - transform.position;
            lift = transform.up * 0.06f;

            lp[0].doLegPounce(true, difference, lift);
            lp[1].doLegPounce(true, difference, lift);

            for (int i = 4; i <= 7; i++)
            {
                lp[i].doLegPounce(false, difference, lift);
            }
            bodyMove = (transform.right * 0.75f) / (times / 2);
        }
        else
        {
            endPoint = lfT[3].position;
            difference = endPoint - transform.position;
            lift = transform.up * 0.06f;

            lp[2].doLegPounce(true, difference, lift);
            lp[3].doLegPounce(true, difference, lift);

            for (int i = 8; i <= 11; i++)
            {
                lp[i].doLegPounce(false, difference, lift);
            }
            bodyMove = (-transform.right * 0.75f) / (times / 2);
        }

        for (int i = 0; i < (times / 2); i++)
        {
            transform.position += bodyMove;

            yield return new WaitForFixedUpdate(); //Wait
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < (times / 2); i++)
        {
            transform.position += difference / times;

            transform.position += lift;

            yield return new WaitForFixedUpdate();
        }

        RaycastHit2D floorDistance = Physics2D.Raycast(transform.position, -transform.up, 10, layerMask);
        while (floorDistance.distance > 1.1f)
        {
            transform.position += difference / times;

            transform.position -= lift;

            //Checks grond distance on both ends
            RaycastHit2D leftDistance = Physics2D.Raycast(ss.lT.position, -transform.up, 20, layerMask);
            RaycastHit2D rightDistance = Physics2D.Raycast(ss.rT.position, -transform.up, 20, layerMask);

            //If distance uneven enough rotate until fixed
            if (Mathf.Round(leftDistance.distance * 10f) / 10f > Mathf.Round(rightDistance.distance * 10f) / 10f)
            {
                transform.Rotate(new Vector3(0, 0, 0.75f));
            }
            else if (Mathf.Round(leftDistance.distance * 10f) / 10f < Mathf.Round(rightDistance.distance * 10f) / 10f)
            {
                transform.Rotate(new Vector3(0, 0, -0.75f));
            }

            floorDistance = Physics2D.Raycast(transform.position, -transform.up, 10, layerMask);
            yield return new WaitForFixedUpdate();
        }

        //Sets correct hover hight
        RaycastHit2D floorDistance2 = Physics2D.Raycast(transform.position, -transform.up, 10, layerMask);

        if (floorDistance2.distance != bh.hoverDist)
        {
            Vector3 wantedPos = new Vector3(floorDistance2.point.x, floorDistance2.point.y, 0) + transform.up * 1.1f;

            transform.position = wantedPos;
        }
        //Sets correct rotation
        for (int i = 1; i <= 20; i++)
        {
            //Checks grond distance on both ends
            RaycastHit2D leftDistance2 = Physics2D.Raycast(ss.lT.position, -transform.up, 10, layerMask);
            RaycastHit2D rightDistance2 = Physics2D.Raycast(ss.rT.position, -transform.up, 10, layerMask);

            //If distance uneven enough rotate until fixed
            if (Mathf.Round(leftDistance2.distance * 10f) / 10f > Mathf.Round(rightDistance2.distance * 10f) / 10f)
            {
                transform.Rotate(new Vector3(0, 0, 0.75f));
            }
            else if (Mathf.Round(leftDistance2.distance * 10f) / 10f < Mathf.Round(rightDistance2.distance * 10f) / 10f)
            {
                transform.Rotate(new Vector3(0, 0, -0.75f));
            }
        }

        StartCoroutine(rearUpSpit(false));
    }

//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    private IEnumerator rearUpSpit(bool spitting)
    {
        choice = 0;
        rb2.velocity = Vector3.zero;

        ss.enabled = false;
        bh.enabled = false;
        sf.enabled = false;

        int times = 15;
        float bRotation;
        float hRotation;
        if (sf.facingLeft)
        {
            if (!spitting)
            {
                bRotation = (-bodySpin - 17) / times;
                hRotation = (-headSpin - 10) / times;
            }
            else
            {
                bRotation = -bodySpin / times;
                hRotation = -headSpin / times;
            }

            lr[0].doRearUp();
            lr[1].doRearUp();
        }
        else
        {
            if (!spitting)
            {
                bRotation = (bodySpin + 17) / times;
                hRotation = (headSpin + 10) / times;
            }
            else
            {
                bRotation = bodySpin / times;
                hRotation = headSpin / times;
            }

            lr[2].doRearUp();
            lr[3].doRearUp();
        }

        yield return new WaitForFixedUpdate(); //Wait
        yield return new WaitForFixedUpdate(); //Wait

        for (int i = 0; i < times; i++)
        {
            transform.Rotate(new Vector3(0, 0, bRotation));
            hT[0].Rotate(new Vector3(0, 0, hRotation));
            hT[1].Rotate(new Vector3(0, 0, hRotation));

            yield return new WaitForFixedUpdate(); //Wait
        }

        for (int i = 0; i < 8; i++)
        {
            yield return new WaitForFixedUpdate(); //Wait
        }

        if (spitting)
        {
            if (sf.facingLeft)
            {
                ps[0].Play();
                for (int i = 25; i >= -25; i -= 25)
                {
                    GameObject acidClone = Instantiate(acidSpit, spT[0].position, Quaternion.Euler(0, 0, i));
                    acidClone.GetComponent<AcidSpitFly>().shootDir = -transform.right * 3.5f;
                }
            }
            else
            {
                ps[1].Play();
                for (int i = 25; i >= -25; i -= 25)
                {
                    GameObject acidClone = Instantiate(acidSpit, spT[1].position, Quaternion.Euler(0, 0, i));
                    acidClone.GetComponent<AcidSpitFly>().shootDir = transform.right * 3.5f;
                }
            }
        }

        yield return new WaitForSeconds(3f); //Wait

        if (sf.facingLeft)
        {
            lr[0].doRearDown();
            lr[1].doRearDown();
        }
        else
        {
            lr[2].doRearDown();
            lr[3].doRearDown();
        }

        yield return new WaitForFixedUpdate(); //Wait
        yield return new WaitForFixedUpdate(); //Wait

        for (int i = 0; i < times; i++)
        {
            transform.Rotate(new Vector3(0, 0, -bRotation));
            hT[0].Rotate(new Vector3(0, 0, -hRotation));
            hT[1].Rotate(new Vector3(0, 0, -hRotation));

            yield return new WaitForFixedUpdate(); //Wait
        }

        ss.enabled = true;
        bh.enabled = true;
        sf.enabled = true;

        curAttackTime = attackTime;
        attacking = false;
    }
}
