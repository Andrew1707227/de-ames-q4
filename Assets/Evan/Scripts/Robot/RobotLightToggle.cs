using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLightToggle : MonoBehaviour
{

    RobotFollow rf;
    GameObject rLight;

    // Start is called before the first frame update
    void Start()
    {
        rf = gameObject.GetComponent<RobotFollow>();
        rLight = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (rf.enabled)
        {
            rLight.SetActive(false);
        }
        else
        {
            rLight.SetActive(true);
        }
    }
}
