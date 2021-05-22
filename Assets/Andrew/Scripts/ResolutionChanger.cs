using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionChanger : MonoBehaviour {

    private Text text;
    public Vector2Int[] resolutionOptions;
    public GameObject resolutionHolder;
    public int currPos;
    public bool isFullScreen;

    void Start() {
        text = GetComponent<Text>();
        if (Application.platform == RuntimePlatform.WebGLPlayer) {
            resolutionHolder.SetActive(false);
            enabled = false;
        }
        isFullScreen = Screen.fullScreen;
        for (int i = resolutionOptions.Length - 1; i >= 0; i--) {
            if (resolutionOptions[i].x <= Display.main.renderingWidth) {
                currPos = i;
                break;
            }
        }
        UpdateResolution();
    }

    public void prev() {
        if (currPos > 0) {
            currPos--;
            UpdateResolution();
        }
    }

    public void next() {
        if (currPos < resolutionOptions.Length - 1) {
            currPos++;
            UpdateResolution();
        }
    }

    public void UpdateResolution() {
        Vector2Int resolution = resolutionOptions[currPos];
        text.text = resolution.x + "px by " + resolution.y + "px";
        Screen.SetResolution(resolution.x, resolution.y, isFullScreen);
    }

    public void toggleFullScreen() {
        isFullScreen = !Screen.fullScreen;
        UpdateResolution();
    }
}
