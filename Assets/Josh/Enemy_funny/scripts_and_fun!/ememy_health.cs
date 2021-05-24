using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ememy_health : MonoBehaviour
{
    //some bunny script ;)
    public float maxLives;
    private float currLives;

    public AudioClip hurt;
    public AudioClip die;
    AudioSource aS;

    private bool debounce;
    private float coolDown;

    //private hitEffect hitEffect;
    //private Animator anim;

    private float aniTimer;
    private bool isDead;
    private Vector3 tempPos;

    // Start is called before the first frame update
    void Start()
    {
        aS = GetComponent<AudioSource>();
       // anim = GetComponent<Animator>();
       // hitEffect = GetComponent<bullets>();
        currLives = maxLives;
        aniTimer = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullets" && !debounce)
        {
            aS.clip = hurt;
            aS.Play();
            debounce = true; //keep it from hitting twice
            if (currLives - 1 <= 0)
            {
                aS.clip = die;
                aS.Play();
                isDead = true;
                if (GetComponent<AstarPath>() != null)
                {
                    GetComponent<AstarPath>().enabled = false;
                    GetComponent<Flippy>().enabled = false;
                }
                GetComponents<PolygonCollider2D>()[0].enabled = false;
                //GetComponents<PolygonCollider2D>()[1].enabled = false;
                //anim.enabled = true;
               // anim.Play("Deadunga");
                tempPos = transform.position;
            }
            //hitEffect.hitEffectStart();
            currLives--;
        }
    }

    void Update()
    {
        Debug.Log(currLives);

        if (debounce) coolDown += Time.deltaTime;
        if (coolDown > .45)
        {
            debounce = false;
            coolDown = 0;
        }
        if (isDead)
        {
            aniTimer += Time.deltaTime;
            transform.position = tempPos;
        }
        else
        {
            tempPos = transform.position;
        }
        if (aniTimer > 1) Destroy(gameObject);
    }
    public float getLives()
    {
        return currLives;
    }
}
