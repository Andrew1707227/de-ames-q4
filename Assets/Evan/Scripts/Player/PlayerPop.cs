using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float maxPops = 3; //Max pop's before death
    public float currentPops = 3; //Current pops left

    public bool respawnBot = true;

    Transform pT;
    Rigidbody2D rb2;
    Rigidbody2D rRb2;
    RobotFollow rf;
    GunShoot gs;
    RadialBarFill rbf;

    public Volume deathFadeVolume;
    public MMUEffects mMUEffects;
    Image Fade;

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
    }

    private void Update()
    {
        if(respawnBot == false && rf.enabled)
        { 
            respawnBot = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag != "SafeTouch")
        {
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
        }

        //If dead
        if(currentPops <= 0)
        {
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn() {
        if (deathFadeVolume.profile.TryGet(out Vignette vignette)) {
            GetComponent<Collider2D>().isTrigger = true;
            GetComponent<PlayerMoveV2>().enabled = false;
            Color temp = Fade.color;
            for (float i = 0; i <= 1; i += 1 / 60f) {
                vignette.intensity.SetValue(new NoInterpFloatParameter(i, true));
                Fade.color = new Color(temp.r, temp.g, temp.b, (i - .75f) * 4);
                yield return new WaitForFixedUpdate();
            }
            //Get rid of pop objects
            foreach (Transform child in popsHolder.transform) {
                Destroy(child.gameObject);
            }
            yield return new WaitForSeconds(1);
            currentPops = 3; //Reset pops
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
