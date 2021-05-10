using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    //Gets volume slider
    public GameObject volumeSlider;

    //Holds master volume
    public static float masterVolume = 0.5f;

    Slider s;

    // Start is called before the first frame update
    void Start()
    {
        s = volumeSlider.GetComponent<Slider>();

        //Sets slider to current volume (No reset)
        s.value = masterVolume;
    }

    // Update is called once per frame
    void Update()
    {
        //Gets masterVolume from slider
        masterVolume = s.value;
        //Sets volume to masterVolume
        AudioListener.volume = masterVolume;
    }
}
