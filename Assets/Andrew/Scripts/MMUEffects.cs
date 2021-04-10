using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMUEffects : MonoBehaviour {
    // Start is called before the first frame update
    private ParticleSystem ps;
    private GameObject player;
    private SpriteRenderer sr;
    private AudioSource Asource;
    void Start() {
        ps = GetComponent<ParticleSystem>();
        player = GameObject.Find("Player");
        sr = player.GetComponent<SpriteRenderer>();
        Asource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        Vector3 pos = transform.localPosition;
        Vector3 coneRot = transform.eulerAngles;
        if (sr.flipX) {
            transform.eulerAngles = new Vector3(coneRot.x, 180, coneRot.z);
            transform.localPosition = new Vector3(-.3f, pos.y, pos.z);
        } else {
            transform.eulerAngles = new Vector3(coneRot.x, 0, coneRot.z);
            transform.localPosition = new Vector3(.3f,pos.y,pos.z);
        }
        if (Input.GetAxis("Horizontal") + Input.GetAxis("Vertical") > .01 && !Asource.isPlaying) {
            Asource.Play();
        }
    }
}
