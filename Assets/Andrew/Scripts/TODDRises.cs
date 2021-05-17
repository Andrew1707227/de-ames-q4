using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TODDRises : MonoBehaviour {
    public GameObject TODD;
    public TextScroller textScroller;
    GameObject player;
    private Rigidbody2D rb2;
    private PlayerMoveV2 move;
    private FollowCamera camFollow;
    private bool debounce = false;

    void Start() {
        player = GameObject.Find("Player");
        rb2 = player.GetComponent<Rigidbody2D>();
        move = player.GetComponent<PlayerMoveV2>();
        PlayerMoveV2.prevSpeed = Vector2.zero;
        camFollow = Camera.main.GetComponent<FollowCamera>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" && !debounce) {
            debounce = true;
            StartCoroutine(TODDGoUp());
        }
    }

    private void LateUpdate() {
        if (!move.enabled) {
            rb2.velocity = Vector2.zero;
        }
    }

    private void FixedUpdate() {
        if (!camFollow.enabled) {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, TODD.transform.position,.05f);
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize -= .05f, 3, 5);
        } else {
            Camera.main.orthographicSize = 5;
        }
    }

    IEnumerator TODDGoUp() {
        PlayerManager.StopMoving();
        Vector3 TODDPos = TODD.transform.position;
        camFollow.enabled = false;
        yield return new WaitForSeconds(.5f);
        for (float i = TODDPos.y; i < TODDPos.y + 1.5f; i += 2/60f ) {
            TODD.transform.position = new Vector3(TODDPos.x, i, TODDPos.z);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(textScroller.RunText(new string[] {"Hello traveller! You must be so confused.", "Since you're here, it's only fair that I properly introduce myself.",
        "My name is T.O.D.D." ,"What does my name stand for you ask?","<d>Well, I'm not telling you, disgusting human.","<d>So you can just go on by."}));
        yield return new WaitUntil(() => textScroller.isTextFinished());
        PlayerManager.StartMoving();
        camFollow.enabled = true;
        yield return new WaitUntil(() => rb2.velocity.magnitude > .25f);
        PlayerManager.StopMoving();
        TODD.GetComponent<RobotFollow>().enabled = true;
        StartCoroutine(textScroller.RunText(new string[] { "Wait!", "Don't leave I....","I need your help.", "I know a place where there's a working spaceship, but I can't repair it alone.",
            "<d>Since you aren't doing anything meaningful,","I figured that you could help.","What do you say?","...              ", "<d>Well, looks like you're the quiet type.", "<d>Typical.", 
            "Just... don't touch the walls ok?"}));
        yield return new WaitUntil(() => textScroller.isTextFinished());
        PlayerManager.StartMoving();
        yield return new WaitForSeconds(.5f);
    }
}
