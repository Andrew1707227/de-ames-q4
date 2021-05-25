using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DisableEnemies : MonoBehaviour {

    private GameObject player;
    public GameObject gfx;
    private Rigidbody2D rb2;

    private AIPath aiPath;
    private Flippy flippy;
    private Collider2D collider1;
    private Collider2D collider2;

    void Start() {
        player = GameObject.Find("Player");
        rb2 = player.GetComponent<Rigidbody2D>();
        aiPath = GetComponent<AIPath>();

        collider1 = gfx.GetComponents<Collider2D>()[0];
        collider2 = gfx.GetComponents<Collider2D>()[1];
        flippy = gfx.GetComponent<Flippy>();
    }

    // Update is called once per frame
    void Update() {
        if (gfx) { 
            aiPath.enabled = !(rb2.constraints == RigidbodyConstraints2D.FreezeAll);
            flippy.enabled = !(rb2.constraints == RigidbodyConstraints2D.FreezeAll);
            if (gfx && !flippy.enabled) {
                collider1.enabled = false;
                collider2.enabled = false;
            }
        }
    }
}
