using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class TextScroller : MonoBehaviour {
    // Start is called before the first frame update
    private AudioSource ASource;
    private Text TextComponent;
    private bool isFinished = false;
    private bool isTextArrayFinished = false;
    public AudioClip defaultClip;
    public AudioClip distortedClip;
    public Image background;
    public ParticleSystem ps;
    Vector2 bSize;
    Vector2 bPos;

    public Sprite angryTODDOpen;
    public Sprite angryTODDClosed;
    public Sprite normalTODDOpen;
    public Sprite normalTODDClosed;
    public SpriteRenderer TODDSr;

    private void Awake() {
        ASource = GetComponent<AudioSource>();
        TextComponent = GetComponent<Text>();
        isFinished = false;
        bSize = background.rectTransform.sizeDelta;
        bPos = background.rectTransform.anchoredPosition;
        TextComponent.text = "";
        background.enabled = false;
        background.material.EnableKeyword("NoiseAmount");
    }
    /// <summary>
    /// Opens up the dialogue box, displays the text, then closes it
    /// (See ExampleDialogue.cs for adding distortion).
    /// </summary>
    /// <param name="text">An array of dialogue lines</param>
    /// <returns>TextScroller.isTextFinished(); will return true when finished.</returns>
    public IEnumerator RunText(string[] text) {
        isTextArrayFinished = false;
        background.enabled = true;
        background.rectTransform.sizeDelta = new Vector2(5, bSize.y);
        background.rectTransform.anchoredPosition = new Vector2(bPos.x, bPos.y - 100);

        for (int i = (int)(bPos.y - 100); i <= bPos.y; i += 12) {
            background.rectTransform.anchoredPosition = new Vector2(bPos.x, i);
            yield return new WaitForFixedUpdate();
        }
        background.rectTransform.anchoredPosition = bPos;
        yield return new WaitForSeconds(.2f);

        for (int i = 5; i <= bSize.x; i += 40) {
            background.rectTransform.sizeDelta = new Vector2(i, bSize.y);
            yield return new WaitForFixedUpdate();
        }
        background.rectTransform.sizeDelta = bSize;

        for (int i = 0; i < text.Length; i++) {
            isFinished = false;
            if (text[i].StartsWith("<d>")) {
                StartCoroutine(GlitchEffect());
                StartCoroutine(WriteText(text[i].Substring(3), distortedClip));
            } else {
                StartCoroutine(WriteText(text[i], defaultClip));
            }
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
        background.rectTransform.anchoredPosition = new Vector2(bPos.x, bPos.y - 100);
        background.enabled = false;
        isTextArrayFinished = true;
    }

    private IEnumerator WriteText(string text, AudioClip tone) {
        ASource.clip = tone;
        for (int i = 0; i < text.Length; i++) {
            TextComponent.text += text.Substring(i, 1);
            ASource.volume = Random.Range(.7f, 1f);
            if (text.Substring(i, 1) != " ") ASource.Play();
            yield return new WaitForFixedUpdate();
            if (tone == distortedClip) {
                TODDSr.sprite = angryTODDClosed;
            } else {
                TODDSr.sprite = normalTODDClosed;
            }
            yield return new WaitForFixedUpdate();
            if (tone == distortedClip) {
                TODDSr.sprite = angryTODDOpen;
            } else {
                TODDSr.sprite = normalTODDOpen;
            }
            ASource.Stop();
        }
        TODDSr.sprite = normalTODDClosed;
        isFinished = true;
    }

    private IEnumerator GlitchEffect() {
        ps.Play();
        int noiseAmount = Shader.PropertyToID("Vector1_82F66C44");
        Material glitchMaterial = background.material;
        for (int i = 100; i >= 0; i -= 10) {
            glitchMaterial.SetFloat(noiseAmount,i);
            yield return new WaitForFixedUpdate();
        }
        ps.Stop();
    }

    public bool isTextFinished() {
        return isTextArrayFinished;
    }
}
