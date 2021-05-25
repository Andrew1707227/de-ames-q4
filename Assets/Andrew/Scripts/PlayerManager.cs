using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    static GameObject player;
    static PlayerMoveV2 move;
    static Rigidbody2D rb2;

    public static void StopMoving() {
        player = GameObject.Find("Player");
        player.GetComponent<PlayerPop>().enabled = false;
        move = player.GetComponent<PlayerMoveV2>();
        rb2 = player.GetComponent<Rigidbody2D>();
        move.enabled = false;
        move.vertTimer = -1;
        move.horizTimer = -1;
        rb2.velocity = Vector2.zero;
        rb2.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public static void StartMoving() {
        player = GameObject.Find("Player");
        player.GetComponent<PlayerPop>().enabled = true;
        move = player.GetComponent<PlayerMoveV2>();
        rb2 = player.GetComponent<Rigidbody2D>();
        rb2.constraints = RigidbodyConstraints2D.FreezeRotation;
        move.enabled = true;
    }
}
