using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {

    public GameObject template;
    public TextScroller textScroller;
    public float fireRate;
    private bool debounce;

    void Start() {
        debounce = true;
    }

    void FixedUpdate() {
        if (Time.frameCount % fireRate == 0) {
            Instantiate(template);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        debounce = true;
    }

    public IEnumerator AsteroidWarning() {
        textScroller.RunText(new string[] {""});
        yield return null;
    }
}
