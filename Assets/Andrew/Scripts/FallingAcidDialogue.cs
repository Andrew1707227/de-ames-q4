using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAcidDialogue : MonoBehaviour {
    public TextScroller textScroller;
    public PlayerMoveV2 move;
    public Rigidbody2D rb2;

    void Start() {
        StartCoroutine(Intro());
    }

    private void Update() {
        if (!move.enabled) {
            rb2.velocity = Vector2.zero;
        }
    }

    private IEnumerator Intro() {
        yield return new WaitForSeconds(.4f);
        StartCoroutine(textScroller.RunText(new string[] {"WAIT!", "The worm is shooting acid at us, we must be careful here.", "<d>Or at least, YOU must be careful.",
            "<d>I on the other hand am invulnerable to acid, so I could care less.","Try to make it out alive!"}));
        yield return new WaitForSeconds(.1f);
        move.enabled = false;
        yield return new WaitUntil(() => textScroller.isTextFinished());
        move.enabled = true;
    }
}
