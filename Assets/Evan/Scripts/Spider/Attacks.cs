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
    public GameObject[] frontTargets = new GameObject[4];

    public float bodySpin;
    public float headSpin;

    public float attackTime = 10;
    float curAttackTime;

    bool shooting = false;

    Rigidbody2D rb2;
    SpiderSpinner ss;
    BodyHover bh;
    SpiderFollow sf;
    Transform pT;
    Transform[] spT = new Transform[2];
    Transform[] hT = new Transform[2];
    ParticleSystem[] ps = new ParticleSystem[2];
    LegRear[] lr = new LegRear[4];

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
        }
        for (int i = 0; i <= 1; i++)
        {
            spT[i] = shootPoints[i].GetComponent<Transform>();
        }
        for (int i = 0; i <= 1; i++)
        {
            hT[i] = heads[i].GetComponent<Transform>();
        }
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
        
        if (curAttackTime <= 0 && !shooting)
        {
            shooting = true;
            StartCoroutine(rearUpSpit());
        }
    }

    private IEnumerator rearUpSpit()
    {
        rb2.velocity = Vector3.zero;

        ss.enabled = false;
        bh.enabled = false;
        sf.enabled = false;

        int times = 15;
        float bRotation;
        float hRotation;
        if (sf.facingLeft)
        {
            bRotation = -bodySpin / times;
            hRotation = -headSpin / times;
            lr[0].doRearUp();
            lr[1].doRearUp();
        }
        else
        {
            bRotation = bodySpin / times;
            hRotation = headSpin / times;
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

        yield return new WaitForFixedUpdate(); //Wait
        yield return new WaitForFixedUpdate(); //Wait
        yield return new WaitForFixedUpdate(); //Wait
        yield return new WaitForFixedUpdate(); //Wait
        yield return new WaitForFixedUpdate(); //Wait
        yield return new WaitForFixedUpdate(); //Wait
        yield return new WaitForFixedUpdate(); //Wait
        yield return new WaitForFixedUpdate(); //Wait

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
        shooting = false;
    }
}
