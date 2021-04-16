using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    public Vector3 offset;
    public GameObject player;
    private bool isTouchingWall;

    void Start() {
        isTouchingWall = false;
    }

    void Update() {
        if (!isTouchingWall) {
            Camera.main.transform.position = player.transform.position + offset;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Wall") isTouchingWall = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Wall") isTouchingWall = false;
    }
}
