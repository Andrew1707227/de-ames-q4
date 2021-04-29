using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour {
    private Animator anim;
    private Rigidbody2D rb2;
    private Collider2D touch;

    void Start() {
        anim = GetComponent<Animator>();
        rb2 = GetComponent<Rigidbody2D>();
        touch = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!(collision.tag == "Trigger" || collision.tag == "Checkpoint")) {
            rb2.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        rb2.velocity = Vector2.zero;
        touch.isTrigger = true;
        anim.enabled = true;
        Destroy(gameObject);
    }
}
