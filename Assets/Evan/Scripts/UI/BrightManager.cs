using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BrightManager : MonoBehaviour
{
    public static float brightnessShift = 0;

    float lastBright = brightnessShift;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "Global Light 2D")
        {
            gameObject.GetComponent<Light2D>().intensity += brightnessShift;
        }
        else
        {
            gameObject.GetComponent<Light2D>().intensity -= brightnessShift;
        }

        lastBright = brightnessShift;
    }

    private void Update()
    {
        if (brightnessShift != lastBright)
        {
            if (gameObject.name == "Global Light 2D")
            {
                gameObject.GetComponent<Light2D>().intensity -= lastBright;
            }
            else
            {
                gameObject.GetComponent<Light2D>().intensity += lastBright;
            }

            if (gameObject.name == "Global Light 2D")
            {
                gameObject.GetComponent<Light2D>().intensity += brightnessShift;
            }
            else
            {
                gameObject.GetComponent<Light2D>().intensity -= brightnessShift;
            }
        }

        lastBright = brightnessShift;
    }
}
