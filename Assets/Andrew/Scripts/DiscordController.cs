using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Discord;

public class DiscordController : MonoBehaviour {

    public Discord.Discord discord;
    public string Location;
    void OnEnable() {
        if (Application.platform == RuntimePlatform.WebGLPlayer) {
            enabled = false;
        } else {
            try {
                TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
                long epoch = (long)(t.TotalSeconds - Time.realtimeSinceStartup);
                discord = new Discord.Discord(847250392340234290, (ulong)CreateFlags.Default);
                var activityManager = discord.GetActivityManager();
                var activity = new Activity {
                    Details = Location,
                    Timestamps = {
                        Start = epoch
                    },
                    Assets = {
                        LargeImage = "icon",
                        LargeText = "An Opportunity"
                    }
                };
                activityManager.UpdateActivity(activity, (res) => {
                    if (res == Result.Ok) Debug.Log("Discord status set.");
                    else Debug.LogWarning("Discord status failed.");
                });

            } catch {}
        }
    }

    void Update() {
        if (Application.platform != RuntimePlatform.WebGLPlayer) {
            try {
                discord.RunCallbacks();
            } catch {} 
        }
    }

    void OnApplicationQuit() {
        if (Application.platform != RuntimePlatform.WebGLPlayer) {
            try {
                discord.Dispose();
            } catch {} 
        }
    }
}
