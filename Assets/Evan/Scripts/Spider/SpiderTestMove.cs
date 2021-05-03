using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderTestMove : MonoBehaviour
{

    Rigidbody2D rb2;

    // Start is called before the first frame update
    void Start()
    {
        rb2 = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        rb2.velocity = -transform.right;

        
        if (Input.GetKeyDown("a"))
        {
            rb2.AddForce(-transform.right, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown("d"))
        {
            rb2.AddForce(transform.right, ForceMode2D.Impulse);
        }
        
    }
}
