using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidShooter : MonoBehaviour {
    public GameObject Acid;
    private Animator anim;
    public float fireRate;
    [Tooltip("Whether or not the acid should fire up to the ceiling.")]
    public bool isUp;
    private SpriteRenderer sr;
    private Sprite defaultSprite;

    void Start() {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        defaultSprite = sr.sprite;
        StartCoroutine(AcidDropper());
    }
    
    private IEnumerator AcidDropper() {
        anim.enabled = true;
        yield return new WaitForSeconds(.25f);
        if (isUp) {
            GameObject acidClone = Instantiate(Acid, transform.position + Vector3.up, Quaternion.Euler(0, 0, 0));
            acidClone.GetComponent<Rigidbody2D>().gravityScale = -1;
        } else {
            Instantiate(Acid, transform.position - Vector3.up, Quaternion.Euler(0, 0, 0));
        }
        yield return new WaitForSeconds(.25f);
        anim.enabled = false;
        sr.sprite = defaultSprite;
        yield return new WaitForSeconds(fireRate);
        yield return new WaitForSeconds(Random.Range(0, 1f));
        StartCoroutine(AcidDropper());
    }
}
