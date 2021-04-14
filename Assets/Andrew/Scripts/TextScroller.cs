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
    public Image background;
    Vector2 bSize;
    Vector2 bPos;

    private void Awake() {
        ASource = GetComponent<AudioSource>();
        defaultClip = ASource.clip;
        TextComponent = GetComponent<Text>();
        isFinished = false;
        bSize = background.rectTransform.sizeDelta;
        bPos = background.rectTransform.anchoredPosition;
        TextComponent.text = "";
        background.enabled = false;
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
        background.enabled = true;
        background.rectTransform.sizeDelta = new Vector2(5, bSize.y);
        background.rectTransform.anchoredPosition = new Vector2(bPos.x, bPos.y - 100);

        for (int i = (int)(bPos.y - 100); i <= bPos.y; i += 12) {
            background.rectTransform.anchoredPosition = new Vector2(bPos.x, i);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(.2f);

        for (int i = 5; i <= bSize.x; i += 40) {
            background.rectTransform.sizeDelta = new Vector2(i, bSize.y);
            yield return new WaitForFixedUpdate();
        }

        for (int i = 0; i < text.Length; i++) {
            isFinished = false;
            StartCoroutine(WriteText(text[i],tone,speed));
            yield return new WaitUntil(() => isFinished);
            yield return new WaitForSeconds(1);
            TextComponent.text = "";
            ASource.Stop();
        }

        TextComponent.text = "";
        background.rectTransform.sizeDelta = bSize;
        background.rectTransform.anchoredPosition = bPos;

        for (int i = (int)bSize.x; i >= 5; i -= 40) {
            background.rectTransform.sizeDelta = new Vector2(i, bSize.y);
            yield return new WaitForFixedUpdate();
        }
        background.rectTransform.sizeDelta = new Vector2(5, bSize.y);
        yield return new WaitForSeconds(.2f);

        for (int i = (int)bPos.y; i >= bPos.y - 100; i -= 12) {
            background.rectTransform.anchoredPosition = new Vector2(bPos.x, i);
            yield return new WaitForFixedUpdate();
        }
        
        background.enabled = false;
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

    public bool isTextFinished() {
        return isTextArrayFinished;
    }
}
