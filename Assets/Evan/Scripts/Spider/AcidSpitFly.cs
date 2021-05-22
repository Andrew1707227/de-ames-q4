using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSpitFly : MonoBehaviour
{
    public GameObject acidEffect;

    [HideInInspector]
    public Vector2 shootDir;


    Rigidbody2D rb2;

    // Start is called before the first frame update
    void Start()
    {
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        rb2.AddRelativeForce(shootDir, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(gameObject, 0.1f);
        StartCoroutine(DestroyAcidSpit());
    }

    private IEnumerator DestroyAcidSpit()
    {
        rb2.velocity = Vector2.zero;
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(.75f);
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }
}
