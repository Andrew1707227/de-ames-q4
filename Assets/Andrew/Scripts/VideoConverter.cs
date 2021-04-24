using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoConverter : MonoBehaviour {
    [Tooltip("Type the name of the path relative to Streaming Assets (aka Videoname.mp4)")]
    public string WebGLVersion;
    [Tooltip("use the .webm version for this")]
    public VideoClip LinuxVersion;

    private VideoPlayer vp;

    void Awake() {
        vp = GetComponent<VideoPlayer>();
        if (Application.platform == RuntimePlatform.WebGLPlayer) {
            vp.url = System.IO.Path.Combine(Application.streamingAssetsPath, WebGLVersion);
        } else if (Application.platform == RuntimePlatform.LinuxPlayer) {
            vp.clip = LinuxVersion;
        } 
    }
}
