using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    TextScroller ee;
    // Start is called before the first frame update
    void Start()
    {
        ee = GetComponent<TextScroller>();
        string[] coolstuff = { "Hello there", "General Kenobi you are a bold one!", "wow thats alotta damage." };
        StartCoroutine(ee.RunText(coolstuff));
    }
}
