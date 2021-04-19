using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialNeedleMove : MonoBehaviour
{
    //Holds reference to arm and fill
    public GameObject dialFill;

    float min = 270;
    float max = 450;

    RectTransform rt;
    RadialBarFill rbf;

    // Start is called before the first frame update
    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
        rbf = dialFill.GetComponent<RadialBarFill>();
    }

    // Update is called once per frame
    void Update()
    {

        float convertedAngle;

        if ((int)rt.rotation.eulerAngles.z < 270)
        {
            convertedAngle = 360 + rt.rotation.eulerAngles.z;
        }
        else
        {
            convertedAngle = rt.rotation.eulerAngles.z;
        }

        if (((1 - rbf.curAmmoPrecent) * (max - min)) + min != convertedAngle)
        {
            rt.Rotate(new Vector3(0, 0, ((((1 - rbf.curAmmoPrecent) * (max - min)) + min) - convertedAngle) / 25));
        }
    }
}
