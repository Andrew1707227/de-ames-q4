using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTouchDialogue : MonoBehaviour {

    public TextScroller textScroller;
    public string[] text;
    private bool debounce;

    void Start() {
        debounce = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!debounce && collision.tag == "Player") {
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
