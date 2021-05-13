using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour {
    public Animator anim;
    public Rigidbody2D rb2;
    public Collider2D touch;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!(collision.tag == "Trigger" || collision.tag == "Checkpoint")) {
            rb2.velocity = Vector2.zero;
            anim.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        StartCoroutine(DestroyAcid());
    }

    private IEnumerator DestroyAcid() {
        rb2.velocity = Vector2.zero;
        touch.isTrigger = true;
        yield return new WaitForSeconds(.75f);
        Destroy(gameObject);
    }
}
