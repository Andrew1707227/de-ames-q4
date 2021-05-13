using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    public GameObject player;
    public GameObject[] frontTargets = new GameObject[4];

    public Vector3 bodyChange;
    public float bodySpin;

    public float attackTime = 7;
    float curAttackTime;

    bool shooting = false;

    Rigidbody2D rb2;
    SpiderSpinner ss;
    BodyHover bh;
    SpiderFollow sf;
    Transform pT;
    LegRear[] lr = new LegRear[4];

    // Start is called before the first frame update
    void Start()
    {
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        ss = gameObject.GetComponent<SpiderSpinner>();
        bh = gameObject.GetComponent<BodyHover>();
        sf = gameObject.GetComponent<SpiderFollow>();
        pT = player.GetComponent<Transform>();

        for (int i = 0; i <= 3; i++)
        {
            lr[i] = frontTargets[i].GetComponent<LegRear>();
        }

        curAttackTime = attackTime;
    }

    // Update is called once per frame
    void Update()
    {
        curAttackTime -= Time.deltaTime;

        if (curAttackTime < attackTime / 2 && !shooting)
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
                shooting = true;
                StartCoroutine(rearUpSpit());
            }
        }
        else if (curAttackTime <= 0 && !shooting)
        {
            shooting = true;
            StartCoroutine(rearUpSpit());
        }
    }

    private IEnumerator rearUpSpit()
    {
        Debug.Log("shoot");

        rb2.velocity = Vector3.zero;

        ss.enabled = false;
        bh.enabled = false;
        sf.enabled = false;

        if (sf.facingLeft)
        {
            lr[0].doRearUp();
            lr[1].doRearUp();
        }
        else
        {
            lr[2].doRearUp();
            lr[3].doRearUp();
        }

        //transform.Rotate(new Vector3(0, 0, -bodySpin));

        int times = 10;
        float rotation;
        if (sf.facingLeft)
        {
            rotation = -bodySpin / times;
        }
        else
        {
            rotation = bodySpin / times;
        }

        for (int i = 0; i < times; i++)
        {
            transform.Rotate(new Vector3(0, 0, rotation));

            Debug.Log(-bodySpin / times);

            //t.position += legChange / times;
            
            yield return new WaitForFixedUpdate(); //Wait
        }
        

        //ftT[i].position += legChange;
        yield return new WaitForFixedUpdate();

        //ss.enabled = true;
        //bh.enabled = true;
        //sf.enabled = true;

        curAttackTime = attackTime;
        //shooting = false;
    }
}
