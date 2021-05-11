using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTouchDialogue : MonoBehaviour {

    public TextScroller textScroller;
    private GameObject player;
    private PlayerMoveV2 move;
    private Rigidbody2D rb2;
    public string[] text;
    private bool debounce;

    void Start() {
        player = GameObject.Find("Player");
        rb2 = player.GetComponent<Rigidbody2D>();
        move = player.GetComponent<PlayerMoveV2>();
        debounce = false;
    }

    private void LateUpdate() {
        if (!move.enabled) {
            rb2.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!debounce) {
            debounce = true;
            StartCoroutine(runTextScroller());
        }

    }
    public IEnumerator runTextScroller() {
        PlayerManager.StopMoving();
        StartCoroutine(textScroller.RunText(text));
        yield return new WaitUntil(() => textScroller.isTextFinished());
        PlayerManager.StartMoving();
    }

}
