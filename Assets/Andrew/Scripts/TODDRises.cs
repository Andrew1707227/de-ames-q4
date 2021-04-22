using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TODDRises : MonoBehaviour {
    public GameObject TODD;
    public MMUEffects MMUEffects;
    public TextScroller textScroller;
    GameObject player;

    void Start() {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            StartCoroutine(TODDGoUp());
        }
    }

    IEnumerator TODDGoUp() {
        player.GetComponent<PlayerMoveV2>().enabled = false;
        MMUEffects.enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Vector3 TODDPos = TODD.transform.position;
        for (float i = TODDPos.y; i < TODDPos.y + 1.5f; i += 1/60f ) {
            TODD.transform.position = new Vector3(TODDPos.x, i, TODDPos.z);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(textScroller.RunText(new string[] {"Hello traveller! You must be so confused.", "Well, welcome to the worm!", "Since you're here, it's only fair that I properly introduce myself.",
        "My name is The Organization Doppler Drone." ,"What's The Organization you ask?","<d>Well, I'm not telling you.","Anyway, you can call me T.O.D.D. for short."}));
        yield return new WaitUntil(() => textScroller.isTextFinished());
        StartCoroutine(textScroller.RunText(new string[] { "Wait!", "Don't leave I....","I need your help.", "I know a place where there's a working spaceship, but I can't repair it alone.",
            "<d>Since you aren't doing anything meaningful,","I figured that you could help.","What do you say?","...              ", "<d>Well, looks like you're the quiet type.", "<d>Typical." }));
        player.GetComponent<PlayerMoveV2>().enabled = true;
        TODD.GetComponent<RobotFollow>().enabled = true;
        MMUEffects.enabled = true;
    }
}
