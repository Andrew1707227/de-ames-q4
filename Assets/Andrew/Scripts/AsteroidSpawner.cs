using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {

    public GameObject[] template;
    public float fireRate;
    private int frameCount;

    void Start() {
        frameCount = 0;
    }

    void FixedUpdate() {
        if (frameCount % fireRate == 0) {
            Instantiate(template[Random.Range(0,template.Length)], new Vector3(9999,9999), new Quaternion(), transform);
            frameCount = 0;
        }
        frameCount++;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            enabled = false;
        }
    }
}
