using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour {
    public Animator anim;
    public Rigidbody2D rb2;
    public Collider2D touch;
    public GameObject acidEffect;

    private void OnCollisionEnter2D(Collision2D collision) {
        StartCoroutine(DestroyAcid());
    }

    private IEnumerator DestroyAcid() {
        rb2.velocity = Vector2.zero;
        Vector3 offset = Vector3.up * .8f;
        if (rb2.gravityScale == -1) {
            offset = Vector3.down * .2f;
        } 
        GameObject acidEffectClone = Instantiate(acidEffect, transform.position + offset, Quaternion.Euler(0, 0, 0));
        acidEffectClone.GetComponent<SpriteRenderer>().flipY = rb2.gravityScale == -1;
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        GetComponent<Collider2D>().isTrigger = true;
        yield return new WaitForSeconds(.75f);
        Destroy(acidEffectClone);
        Destroy(gameObject);
    }
}
