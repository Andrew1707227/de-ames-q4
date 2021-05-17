using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSpitFly : MonoBehaviour
{
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
        Destroy(gameObject, 0.1f);
    }
}
