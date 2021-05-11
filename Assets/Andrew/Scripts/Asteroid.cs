using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public float yRange;
    public float yPos;
    public float speed;
    private bool debounce;

    void Start() {
        transform.position = new Vector3(65, yPos + Random.Range(-yRange, yRange), transform.position.z);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!debounce) {
            debounce = true;
            Destroy(gameObject);
        }
    }
}
