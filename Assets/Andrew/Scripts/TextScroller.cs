using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScroller : MonoBehaviour {
    // Start is called before the first frame update
    private AudioSource ASource;
    private Text TextComponent;
    private bool isFinished = false;
    private bool isTextArrayFinished = false;
    private AudioClip defaultClip;

    private void Awake() {
        ASource = GetComponent<AudioSource>();
        defaultClip = ASource.clip;
        TextComponent = GetComponent<Text>();
        isFinished = false;
        CloseTextBox();
    }

    public IEnumerator RunText(string[] text) {
        StartCoroutine(RunText(text, defaultClip, 0.05f));
        yield return null;
    }

    public IEnumerator RunText(string[] text, AudioClip tone) {
        StartCoroutine(RunText(text, tone, 0.05f));
        yield return null;
    }

    public IEnumerator RunText(string[] text, AudioClip tone, float speed) {
        isTextArrayFinished = false;
        for (int i = 0; i < text.Length; i++) {
            isFinished = false;
            StartCoroutine(WriteText(text[i],tone,speed));
            yield return new WaitUntil(() => isFinished);
            yield return new WaitForSeconds(2);
            TextComponent.text = "";
            ASource.Stop();
        }
        CloseTextBox();
        isTextArrayFinished = true;
    }

    private IEnumerator WriteText(string text, AudioClip tone, float speed) {
        ASource.clip = tone;
        for (int i = 0; i < text.Length; i++) {
            TextComponent.text = TextComponent.text + text.Substring(i, 1);
            ASource.volume = Random.Range(.7f, 1f);
            if (text.Substring(i, 1) != " ") ASource.Play();
            yield return new WaitForSeconds(speed);
            ASource.Stop();
        }
        isFinished = true;
    }

    public void CloseTextBox() {
        TextComponent.text = "";
    }

    public bool isTextFinished() {
        return isTextArrayFinished;
    }
}
