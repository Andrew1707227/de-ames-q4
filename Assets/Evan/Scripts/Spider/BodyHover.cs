using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHover : MonoBehaviour
{
    public float hoverDist = 1.1f;

    //int layerMask;
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        //Get layermask
        //layerMask = ~LayerMask.GetMask("Spider");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D floorDistance = Physics2D.Raycast(transform.position, -transform.up, 10, layerMask);

        if (floorDistance.distance != hoverDist)
        {
            Vector3 wantedPos = new Vector3(floorDistance.point.x, floorDistance.point.y, 0) + (transform.up * 1.1f);

            // Debug.Log(wantedPos);

            Vector3 difference = wantedPos - transform.position;

            transform.position += (wantedPos - transform.position) / 10;
            

            //transform.position = wantedPos;
        }
    }
}
