using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleDialogue : MonoBehaviour {
    TextScroller textScroller;

    void Start() {
        textScroller = GetComponent<TextScroller>();
        StartCoroutine(ExampleText());
    }
    private IEnumerator ExampleText() {
        yield return new WaitForSeconds(1);
        //Type <d> at the start in order to use the distored audio (typing nothing uses the default)
        StartCoroutine(textScroller.RunText(new string[] { "Hello there! My name is T.O.D.D.", "I've been in this slimy worm for a long time,", 
            "so sometimes ill malfunction and says things like", "<d>You're trash."}));
        yield return new WaitUntil(() => textScroller.isTextFinished());
        StartCoroutine(textScroller.RunText(new string[] { "<d>Stings doesn't it?", "But what was I gonna say?","Oh yes, Depresso Espresso is pretty cool you should check them out.", 
            "<d>And also you smell bad.","Goodbye!" }));
    }
}
