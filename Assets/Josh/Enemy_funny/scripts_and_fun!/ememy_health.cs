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

    private SpriteRenderer sr;

    //private hitEffect hitEffect;
    //private Animator anim;

    private float aniTimer;
    private bool isDead;
    private Vector3 tempPos;

    // Start is called before the first frame update
    void Start()
    {
        aS = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
       // anim = GetComponent<Animator>();
       // hitEffect = GetComponent<bullets>();
        currLives = maxLives;
        aniTimer = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullets" && !debounce)
        {
            debounce = true; //keep it from hitting twice
            if (!aS.isPlaying && !isDead) aS.PlayOneShot(hurt);

            if (currLives - 1 <= 0)
            {
                if (sr.flipX) {
                    var shape = GetComponent<ParticleSystem>().shape;
                    shape.position = new Vector3(.15f, -.72f, 0);
                }
                GetComponent<ParticleSystem>().Play();
                StartCoroutine(Fade());
                if (!isDead) aS.PlayOneShot(die);
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
    
    private IEnumerator Fade() {
        for (float i = 1; i >= 0; i -= 1/60f) {
            sr.color = new Color(1, 1, 1, i);
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
    }

    void Update()
    {

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
        //if (aniTimer > 1) Destroy(gameObject);
    }
    public float getLives()
    {
        return currLives;
    }
}
