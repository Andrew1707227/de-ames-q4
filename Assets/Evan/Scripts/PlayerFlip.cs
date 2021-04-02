using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    //Holds parent spriteRenderer referecne
    SpriteRenderer pSR;

    //Reference to main Camera
    public Camera cam;

    //Holds mouse position
    Vector2 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        pSR = gameObject.transform.parent.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Sets mousePos to current mouse position
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //Flip player based on aiming direction
        if (mousePos.x > transform.position.x)
        {
            pSR.flipX = true;
        }

        if (mousePos.x < transform.position.x)
        {
            pSR.flipX = false;
        }
    }
}
