using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerPop : MonoBehaviour
{
    //Gets popPrefab and where to put it
    public GameObject pop;
    public GameObject popsHolder;

    //All objects that need reseting
    public GameObject arm;
    public Vector3 checkpoint;
    public GameObject robot;
    public GameObject dial;

    public float maxPops = 5; //Max pop's before death
    public float currentPops = 5; //Current pops left
    public float invulnTime = 1f; //Time invuln after hit
    float currentInvuln = 0;

    public bool respawnBot = true;

    [HideInInspector]
    public bool popsDead;

    Transform pT;
    Rigidbody2D rb2;
    Rigidbody2D rRb2;
    RobotFollow rf;
    GunShoot gs;
    RadialBarFill rbf;

    public Volume deathFadeVolume;
    public MMUEffects mMUEffects;
    private Sprite defaultSprite;
    private Sprite defaultArm;
    Image Fade;

    public AudioClip playerDamage;
    public SpriteRenderer TODDSr;
    public Sprite damageTODD;
    private AudioSource Asource;

    // Start is called before the first frame update
    void Start()
    {
        pT = popsHolder.GetComponent<Transform>();
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        rRb2 = robot.GetComponent<Rigidbody2D>();
        rf = robot.GetComponent<RobotFollow>();
        gs = arm.GetComponent<GunShoot>();
        rbf = dial.GetComponent<RadialBarFill>();

        Fade = GameObject.Find("Fade").GetComponent<Image>();
        defaultSprite = GetComponent<SpriteRenderer>().sprite;
        defaultArm = arm.GetComponent<SpriteRenderer>().sprite;
        Asource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(respawnBot == false && rf.enabled)
        { 
            respawnBot = true;
        }

        if (currentInvuln > 0)
        {
            currentInvuln -= Time.deltaTime;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag != "SafeTouch" && currentInvuln <= 0)
        {
            //Start invulTimer
            currentInvuln = invulnTime;

            //Increment pops
            currentPops--;

            //Gets collision point
            ContactPoint2D contact = collision.GetContact(0);

            //Turns collision normal into Quaternion
            Quaternion qNormal = Quaternion.LookRotation(new Vector3(0, 0, 1), contact.normal);

            //Creates pop object
            GameObject newPop = Instantiate(pop, contact.point, qNormal, pT);

            //Gives pop object push direction
            newPop.GetComponent<PopPush>().pushDirection = contact.normal;

            Asource.PlayOneShot(playerDamage);
            StartCoroutine(ChangeSprite());
        }

        //If dead and not boss dead
        if(currentPops <= 0 && !popsDead)
        {
            StartCoroutine(Respawn());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.tag != "SafeTouch" && currentInvuln <= 0) {
            //Start invulTimer
            currentInvuln = invulnTime;

            //Increment pops
            currentPops--;

            //Gets collision point
            ContactPoint2D contact = collision.GetContact(0);

            //Turns collision normal into Quaternion
            Quaternion qNormal = Quaternion.LookRotation(new Vector3(0, 0, 1), contact.normal);

            //Creates pop object
            GameObject newPop = Instantiate(pop, contact.point, qNormal, pT);

            //Gives pop object push direction
            newPop.GetComponent<PopPush>().pushDirection = contact.normal;

            Asource.PlayOneShot(playerDamage);
            StartCoroutine(ChangeSprite());
        }

        //If dead and not boss dead
        if (currentPops <= 0 && !popsDead) {
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator ChangeSprite() {
        Sprite temp = TODDSr.sprite;
        yield return new WaitForSeconds(.1f);
        TODDSr.sprite = damageTODD;
        yield return new WaitForSeconds(.5f);
        TODDSr.sprite = temp;
    }

    private IEnumerator Respawn() {
        if (deathFadeVolume.profile.TryGet(out Vignette vignette)) {
            GetComponent<Collider2D>().isTrigger = true;
            GetComponent<PlayerMoveV2>().enabled = false;
            GetComponent<AudioSource>().Play();
            //GetComponent<Animator>().enabled = true;
            GetComponent<Animator>().Play("PlayerDeath");
            Color temp = Fade.color;
            for (float i = 0; i <= 1; i += 1 / 60f) {
                vignette.intensity.SetValue(new NoInterpFloatParameter(i, true));
                Fade.color = new Color(temp.r, temp.g, temp.b, (i - .75f) * 4);
                yield return new WaitForFixedUpdate();
            }

            //if in bossfight
            if (SceneManager.GetActiveScene().name == "BossFight")
            {
                //Reload scene
                SceneManager.LoadScene("BossFight");
            }

            //Get rid of pop objects
            foreach (Transform child in popsHolder.transform) {
                Destroy(child.gameObject);
            }
            GetComponent<Animator>().Play("PlayerIdle");
            GetComponent<SpriteRenderer>().sprite = defaultSprite;
            arm.GetComponent<SpriteRenderer>().sprite = defaultArm;
            yield return new WaitForSeconds(1);
            currentPops = maxPops; //Reset pops
            //Reset position
            transform.position = new Vector3(checkpoint.x, checkpoint.y, transform.position.z);
            GetComponent<Collider2D>().isTrigger = false;
            GetComponent<PlayerMoveV2>().enabled = true;
            yield return new WaitForFixedUpdate();
            rbf.i.fillAmount = rbf.maxValue; //Reset dial fill
            rb2.velocity = Vector2.zero; //Reset velocity
            if (respawnBot) {
                //rRb2.transform.position = checkpoint + rf.offset; //Reset robot position
            }
            gs.currentAmmo = gs.maxAmmo; //Reset ammo
            for (float i = 1; i >= 0; i -= 1 / 60f) {
                vignette.intensity.SetValue(new NoInterpFloatParameter(i, true));
                Fade.color = new Color(temp.r, temp.g, temp.b, (i - .75f) * 4);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
