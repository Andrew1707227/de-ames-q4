using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerSight : MonoBehaviour
{
    //Holds where the gun is looking
    Vector2 lookDir;

    PlayerAim pa;
    LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        pa = gameObject.GetComponent<PlayerAim>();
        lr = gameObject.GetComponent<LineRenderer>();

        gameObject.GetComponent<LineRenderer>().material.SetColor("_Color", new Color(1f, 1f, 1f, 0.6f));
    }

    // Update is called once per frame
    void Update()
    {
        //Gets look diection and makes if face the right way for this context
        lookDir = pa.currentLookDir.normalized;
        lookDir = new Vector2(lookDir.y, -lookDir.x);

        //Set line start point to players current position
        lr.SetPosition(0, gameObject.transform.position);//.x, gameObject.transform.position.y, 0));

        //Get the layermask
        int layerMask = ~LayerMask.GetMask("Player");

        //Send ray out to find colliders
        RaycastHit2D lazerGetter = Physics2D.Raycast(gameObject.transform.position, lookDir, 30, layerMask);

        //If the ray it and object
        if (lazerGetter.point != Vector2.zero)
        {
            //Set line render end point to lazerGetter hitpoint
            lr.SetPosition(1, lazerGetter.point);
        }
        else //Find screen edge if ray didnt find an object
        {
            //Send ray out to find screen edges
            Ray screenEdgeRay = new Ray(gameObject.transform.position, lookDir);

            float currentMinDistance = float.MaxValue; //Holds the current minimum distance to screen edge
            Vector3 hitPoint = Vector3.zero; //Holds current screen edge hitpoint
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main); //Holds screen edges

            //Checks all planes on the sides of the camera
            for (int i = 0; i < 4; i++)
            {
                //Checks if raycast hit this plane and get how far the hit was
                if (planes[i].Raycast(screenEdgeRay, out var distance))
                {
                    //If new distance is shorter than old distance
                    if (distance < currentMinDistance)
                    {
                        hitPoint = screenEdgeRay.GetPoint(distance); //Set hitpoint to new hitpoint
                        currentMinDistance = distance; //Set currentMinimunDistance to new minimun hit distance
                    }
                }
            }

            //Set line render end point to hitpoint
            lr.SetPosition(1,hitPoint);
        }

    }
}
