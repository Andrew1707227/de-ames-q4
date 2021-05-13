using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public float xPos;
    public float yRange;
    public float yPos;
    public float speed;
    private bool debounce;
    private Rigidbody2D rb2;

    void Start() {
        transform.position = new Vector3(xPos, yPos + Random.Range(-yRange, yRange), transform.position.z);
        rb2 = GetComponent<Rigidbody2D>();
        rb2.AddForce(new Vector2(-speed, 0));
        rb2.angularVelocity = 360;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!debounce) {
            debounce = true;
            StartCoroutine(Delete());
        }
    }
    
    private IEnumerator Delete() {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        Destroy(gameObject);
    }
}
