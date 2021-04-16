using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    public Vector3 offset;
    public float speed;
    public GameObject player;

    void FixedUpdate() {
        Vector3 desiredPos = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, speed);
    }
}
