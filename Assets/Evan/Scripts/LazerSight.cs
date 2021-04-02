using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerSight : MonoBehaviour
{
    PlayerAim pa;

    Vector2 lookDir;

    // Start is called before the first frame update
    void Start()
    {
        pa = gameObject.GetComponent<PlayerAim>();
    }

    // Update is called once per frame
    void Update()
    {
        lookDir = pa.currentLookDir;

        RaycastHit2D lazerGetter = Physics2D.Raycast(gameObject.transform.position, lookDir);
    }
}
