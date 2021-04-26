using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

    public Image Fade;
    public string SceneName;
    public bool startDark;

    void Start() {
       if (startDark) {
            StartCoroutine(BlackToFade());
        } 
    }

    void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            StartCoroutine(FadeToBlack());
        }
    }
    private IEnumerator FadeToBlack() {
        Color temp = Fade.color;
        for (float i = 0; i <= 1; i += 1/60f) {
            Fade.color = new Color(temp.r, temp.g, temp.b, i);
            yield return new WaitForFixedUpdate();
        }
        Fade.color = new Color(temp.r, temp.g, temp.b, 1);
        PlayerMoveV2.prevSpeed = GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity;
        SceneManager.LoadScene(SceneName);
    }
    private IEnumerator BlackToFade() {
        Color temp = Fade.color;
        for (float i = 1; i >= 0; i -= 1/60f) {
            Fade.color = new Color(temp.r, temp.g, temp.b, i);
            yield return new WaitForFixedUpdate();
        }
        Fade.color = new Color(temp.r, temp.g, temp.b, 0);
    }
}
