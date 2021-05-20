using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour {

    public GameObject robot;
    private RobotFollow roboMove;
    public GameObject door;
    private GameObject player;
    public bool keyFound;
    private bool debounce;

    public Sprite OpenDoorSprite;
    public TextScroller textScroller;

    void Start() {
        player = GameObject.Find("Player");
        roboMove = robot.GetComponent<RobotFollow>();
        keyFound = false;
        debounce = false;
    }

    void Update() {
       if (keyFound) {
            transform.position = robot.transform.position;
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!debounce) {
            if (!keyFound && collision.tag == "Player") {
                StartCoroutine(GrabKey());
            }
            else if (collision.name == "Door") {
                StartCoroutine(OpenDoor());
            }
        }
    }

    private IEnumerator GrabKey() {
        debounce = true;
        roboMove.enabled = false;
        Vector2 keyPos = transform.position;
        for (int i = 0; i < 30; i++) {
            robot.transform.position = Vector2.MoveTowards(robot.transform.position, keyPos,.25f);
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(textScroller.RunText(new string[] {"Looks like we found the keycard.", "Let's head back."}));
        roboMove.enabled = true;
        keyFound = true;
        debounce = false;
    }

    private IEnumerator OpenDoor() {
        debounce = true;
        roboMove.enabled = false;
        Vector2 doorPos = door.transform.position;
        for (int i = 0; i < 30; i++) {
            robot.transform.position = Vector2.MoveTowards(robot.transform.position, doorPos, .25f);
            yield return new WaitForFixedUpdate();
        }
        roboMove.enabled = true;
        door.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(.25f);
        door.GetComponent<Animator>().enabled = false;
        door.GetComponent<SpriteRenderer>().sprite = OpenDoorSprite;
        door.GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }
}
