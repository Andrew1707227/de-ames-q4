using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpinner : MonoBehaviour
{

    public GameObject leftCheck;
    public GameObject rightCheck;

    int layerMask;

    Transform lT;
    Transform rT;
    Transform tt;

    // Start is called before the first frame update
    void Start()
    {
        lT = leftCheck.GetComponent<Transform>();
        rT = rightCheck.GetComponent<Transform>();

        //Get layermask
        layerMask = ~LayerMask.GetMask("Spider");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D leftDistance = Physics2D.Raycast(lT.position, -transform.up, 10, layerMask);
        RaycastHit2D rightDistance = Physics2D.Raycast(rT.position, -transform.up, 10, layerMask);

        if (Mathf.Round(leftDistance.distance * 10f) / 10f > Mathf.Round(rightDistance.distance * 10f) / 10f)
        {
            transform.Rotate(new Vector3(0, 0, 0.75f));
        }
        else if (Mathf.Round(leftDistance.distance * 10f) / 10f < Mathf.Round(rightDistance.distance * 10f) / 10f)
        {
            transform.Rotate(new Vector3(0, 0, -0.75f));
        }
    }
}
