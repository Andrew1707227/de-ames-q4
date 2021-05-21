using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossIntro : MonoBehaviour
{

    public GameObject player;
    public GameObject effects;
    public GameObject spiderBody;
    public GameObject targets;
    public GameObject introSpiderBody;
    public GameObject introTargets;
    public GameObject ship;
    public GameObject bossBarFill;
    public GameObject bossBarBorder;
    public GameObject door;
    public Camera mainCamera;

    Vector3 xStartBody = new Vector3(5.71f, -2.82f, 0);
    Vector3 xStartTarg = new Vector3(7.757772f, -1.228127f, 0);

    Transform eT;
    Transform pT;
    Animator a;
    Rigidbody2D rb2;
    FollowCamera fc;
    ParticleSystem ps;
    AudioSource aSource;
    Image fI;
    Image bI;


    // Start is called before the first frame update
    void Start()
    {
        a = door.GetComponent<Animator>();
        eT = effects.GetComponent<Transform>();
        pT = player.GetComponent<Transform>();
        rb2 = player.GetComponent<Rigidbody2D>();
        fc = mainCamera.GetComponent<FollowCamera>();
        ps = effects.GetComponent<ParticleSystem>();
        aSource = effects.GetComponent<AudioSource>();
        fI = bossBarFill.GetComponent<Image>();
        bI = bossBarBorder.GetComponent<Image>();

        PlayerMoveV2.prevSpeed = Vector2.zero;

        StartCoroutine(bossIntro());
    }

    private IEnumerator bossIntro()
    {
        yield return new WaitForSeconds(0.5f);

        rb2.AddForce(new Vector2(0, 2f), ForceMode2D.Impulse);

        float zRot = 0;
        float xPos = .3f;
        float yPos = -.03f;

        Vector3 pos = eT.localPosition;
        Vector3 coneRot = eT.eulerAngles;

        xPos = -.24f;
        yPos = -.31f;
        zRot = 270;

        eT.eulerAngles = new Vector3(coneRot.x, coneRot.y, zRot);
        eT.localPosition = new Vector3(xPos, yPos, pos.z);

        ps.Play();
        aSource.pitch = Mathf.Clamp(rb2.velocity.magnitude / 2, .8f, 1.2f) + Random.Range(-.05f, .05f);
        aSource.Play();


        yield return new WaitForSeconds(1.4f);
        a.enabled = true;
        yield return new WaitForSeconds(0.6f);

        xPos = -.24f;
        yPos = .31f;
        zRot = 90;

        eT.eulerAngles = new Vector3(coneRot.x, coneRot.y, zRot);
        eT.localPosition = new Vector3(xPos, yPos, pos.z);

        ps.Play();
        aSource.pitch = Mathf.Clamp(rb2.velocity.magnitude / 2, .8f, 1.2f) + Random.Range(-.05f, .05f);
        aSource.Play();

        rb2.velocity = Vector2.zero;
        player.GetComponent<PolygonCollider2D>().enabled = true;

        yield return new WaitForSeconds(0.6f);

        fc.player = ship;

        yield return new WaitForSeconds(2.4f);

        fc.player = introSpiderBody;

        for (float i = 0; i <= 1; i += 1 / 120f)
        {
            fI.color = new Color(fI.color.r, fI.color.g, fI.color.b, i);
            bI.color = new Color(bI.color.r, bI.color.g, bI.color.b, i);
            yield return new WaitForFixedUpdate();
        }
        fI.color = new Color(fI.color.r, fI.color.g, fI.color.b, 1);
        bI.color = new Color(bI.color.r, bI.color.g, bI.color.b, 1);

        yield return new WaitForSeconds(0.5f);

        fc.player = player;
        player.GetComponent<PlayerMoveV2>().enabled = true;

        introSpiderBody.SetActive(false);
        introTargets.SetActive(false);

        introSpiderBody.GetComponent<Transform>().position = new Vector3(100, 100, 0);
        introTargets.GetComponent<Transform>().position = new Vector3(100, 100, 0);

        spiderBody.GetComponent<Transform>().position = xStartBody;
        targets.GetComponent<Transform>().position = xStartBody;

        spiderBody.SetActive(true);
        targets.SetActive(true);

        
    }
}
