using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextUnblur : MonoBehaviour
{
    public Font[] fonts;

    void Start()
    {
        //Unfilters fonts
        for (int i = 0; i < fonts.Length; i++)
        {
            fonts[i].material.mainTexture.filterMode = FilterMode.Point;
        }
    }
}
