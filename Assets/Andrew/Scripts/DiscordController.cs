using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Discord;

public class DiscordController : MonoBehaviour {

    public Discord.Discord discord;

    void Start() {
        if (Application.platform == RuntimePlatform.WebGLPlayer) {
            enabled = false;
        } else {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            long epoch = (long)t.TotalSeconds;
            discord = new Discord.Discord(847250392340234290, (ulong)CreateFlags.Default);
            var activityManager = discord.GetActivityManager();
            var activty = new Activity {
                Details = "In Game",
                Timestamps = {
                Start = epoch
            },
                Assets = {
                LargeImage = "icon",
                LargeText = "An Opportunity"
            }

            };
            activityManager.UpdateActivity(activty, (res) => {
                if (res == Result.Ok) Debug.Log("Discord status set.");
                else Debug.LogWarning("Discord status failed.");
            });
        }
    }

    void Update() {
        if (Application.platform != RuntimePlatform.WebGLPlayer)
            discord.RunCallbacks();
    }

    void OnApplicationQuit() {
        if (Application.platform != RuntimePlatform.WebGLPlayer)
            discord.Dispose();
    }
}
