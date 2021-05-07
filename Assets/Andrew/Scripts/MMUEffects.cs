using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMUEffects : MonoBehaviour {
    private ParticleSystem ps;
    private AudioSource Asource;

    private GameObject player;
    private PlayerMoveV2 move;
    private SpriteRenderer sr;
    private Rigidbody2D rb2;

    private float zRot = 0;
    private float xPos = .3f;
    private float yPos = -.03f;

    private float maxHoriz;
    private float maxVert;

    /// <summary>
    /// ignore the input value if it's basically zero
    /// </summary>
    private const float dead = .01f;

    void Start() {
        ps = GetComponent<ParticleSystem>();
        Asource = GetComponent<AudioSource>();

        player = GameObject.Find("Player");
        move = player.GetComponent<PlayerMoveV2>();
        sr = player.GetComponent<SpriteRenderer>();
        rb2 = player.GetComponent<Rigidbody2D>();

        maxHoriz = move.horizTimer;
        maxVert = move.vertTimer;
    }
    
    void Update() {
        float horiz = move.horizTimer;
        float vert = move.vertTimer;
        if ((horiz == Mathf.Clamp(horiz, maxHoriz - .2f, maxHoriz - .05f) || vert == Mathf.Clamp(vert, maxVert - .2f, maxVert - .05f)) && !Asource.isPlaying) {
            Vector3 pos = transform.localPosition;
            Vector3 coneRot = transform.eulerAngles;

            if (sr.flipX) {
                if (Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Abs(Input.GetAxis("Vertical")) + .1) {
                    if (Input.GetAxis("Horizontal") > dead) {
                        xPos = -.42f;
                        yPos = -.03f;
                        zRot = 180;

                    } else if (Input.GetAxis("Horizontal") < -dead) {
                        xPos = -.24f;
                        yPos = -.03f;
                        zRot = 0;
                    }

                } else if (Input.GetAxis("Vertical") > dead) {
                    xPos = -.24f;
                    yPos = -.31f;
                    zRot = 270;

                } else if (Input.GetAxis("Vertical") < -dead) {
                    xPos = -.24f;
                    yPos = .31f;
                    zRot = 90;
                }

            } else {
                if (Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Abs(Input.GetAxis("Vertical")) + .1) {
                    if (Input.GetAxis("Horizontal") > dead) {
                        xPos = .24f;
                        yPos = -.03f;
                        zRot = 180;

                    } else if (Input.GetAxis("Horizontal") < -dead) {
                        xPos = .42f;
                        yPos = -.03f;
                        zRot = 0;
                    }

                } else if (Input.GetAxis("Vertical") > dead) {
                    xPos = .24f;
                    yPos = -.31f;
                    zRot = 270;

                } else if (Input.GetAxis("Vertical") < -dead) {
                    xPos = .24f;
                    yPos = .31f;
                    zRot = 90;
                }
            }
            transform.eulerAngles = new Vector3(coneRot.x, coneRot.y, zRot);
            transform.localPosition = new Vector3(xPos, yPos, pos.z);
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > dead || Mathf.Abs(Input.GetAxis("Vertical")) > dead) {
                ps.Play();
                Asource.pitch = Mathf.Clamp(rb2.velocity.magnitude / 2, .8f, 1.2f) + Random.Range(-.05f, .05f);
                Asource.Play();
            }
        }
    }
}
