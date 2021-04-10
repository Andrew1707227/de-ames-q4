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

    void Start() {
        ps = GetComponent<ParticleSystem>();
        Asource = GetComponent<AudioSource>();

        player = GameObject.Find("Player");
        move = player.GetComponent<PlayerMoveV2>();
        sr = player.GetComponent<SpriteRenderer>();
        rb2 = player.GetComponent<Rigidbody2D>();
    }
    
    void Update() {
        if ((move.horizTimer < .1 || move.vertTimer < .1) && !Asource.isPlaying) {
            Vector3 pos = transform.localPosition;
            Vector3 coneRot = transform.eulerAngles;

            if (sr.flipX) {
                if (Mathf.Abs(rb2.velocity.x) > Mathf.Abs(rb2.velocity.y) + .1) {
                    xPos = -.3f;
                    yPos = -.03f;
                    zRot = 180;

                } else if (rb2.velocity.y > 0) {
                    xPos = -.175f;
                    yPos = -.25f;
                    zRot = 270;

                } else {
                    xPos = -.175f;
                    yPos = .25f;
                    zRot = 90;
                }

            } else {
                if (Mathf.Abs(rb2.velocity.x) > Mathf.Abs(rb2.velocity.y) + .1) {
                    xPos = .3f;
                    yPos = -.03f;
                    zRot = 0;

                } else if (rb2.velocity.y > 0) {
                    xPos = .175f;
                    yPos = -.25f;
                    zRot = 270;

                } else {
                    xPos = .175f;
                    yPos = .25f;
                    zRot = 90;
                }
            }
            transform.eulerAngles = new Vector3(coneRot.x, coneRot.y, zRot);
            transform.localPosition = new Vector3(xPos, yPos, pos.z);
            ps.Play();
            Asource.Play();
        }
    }
}
