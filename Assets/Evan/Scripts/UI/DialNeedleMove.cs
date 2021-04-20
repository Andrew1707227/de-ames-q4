using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialNeedleMove : MonoBehaviour
{
    //Holds reference to arm and fill
    public GameObject dialFill;

    float min = 270; //Minimum angle
    float max = 450; //Max angle

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
        //Holds angle converted to match this: (1 - rbf.curPopPrecent) * (max - min)) + min
        float convertedAngle;

        //If this gameoject's rotation is less than 270
        if ((int)rt.rotation.eulerAngles.z < 270)
        {
            //Add 360 and set it to converted angle
            convertedAngle = 360 + rt.rotation.eulerAngles.z;
        }
        else //If greater than 270
        {
            //Set it to converted angle
            convertedAngle = rt.rotation.eulerAngles.z;
        }

        //Check if current rotation equals wanted rotation
        //Current: convertedAngle
        //Wanted: (1 - rbf.curPopPrecent) * (max - min)) + min 
        if (((1 - rbf.curPopPrecent) * (max - min)) + min != convertedAngle)
        {
            //Rotate gameobject to wanted position (divided by 300 to slow down the turn)
            rt.Rotate(new Vector3(0, 0, ((((1 - rbf.curPopPrecent) * (max - min)) + min) - convertedAngle) / 75));
        }
    }
}
