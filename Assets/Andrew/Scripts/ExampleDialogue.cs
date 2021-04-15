using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleDialogue : MonoBehaviour {
    TextScroller textScroller;
    public AudioClip distort;

    void Start() {
        textScroller = GetComponent<TextScroller>();
        StartCoroutine(ExampleText());
    }
    private IEnumerator ExampleText() {
        yield return new WaitForSeconds(1);
        StartCoroutine(textScroller.RunText(new string[] { "Hello there", "General Kenobi you are a bold one!", "<d>You're trash."}));
        yield return new WaitUntil(() => textScroller.isTextFinished());
        StartCoroutine(textScroller.RunText(new string[] { "<d>Stings doesn't it?", "<d>See ya chump." }));
    }
}
