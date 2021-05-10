using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerSight : MonoBehaviour
{
    //Holds where the gun is looking
    Vector2 lookDir;

    //Holds new gradients
    Gradient gradient = new Gradient();

    PlayerAim pa;
    LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesHitTriggers = false;

        pa = gameObject.GetComponent<PlayerAim>();
        lr = gameObject.GetComponent<LineRenderer>();

        lr = GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
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
        RaycastHit2D lazerGetter = Physics2D.Raycast(gameObject.transform.position, lookDir, 300, layerMask);

        //Send ray out to find screen edges
        Ray screenEdgeRay = new Ray(gameObject.transform.position, lookDir);

        float currentMinDistance = float.MaxValue; //Holds the current minimum distance to screen edge
        Vector3 hitPoint = Vector3.zero; //Holds current screen edge hitpoint
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main); //Holds screen edges

        //Debug.Log(lazerGetter.point);

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

        //Set line render end point to lazerGetter hitpoint
        lr.SetPosition(1, lazerGetter.point);
      
        //If the ray hit an object
        if (lazerGetter.point != Vector2.zero) //&& !(lazerGetter.point.magnitude > hitPoint.magnitude))
        {
            //Set line render end point to lazerGetter hitpoint
            lr.SetPosition(1, lazerGetter.point);
        }
        else //Find screen edge if ray didnt find an object
        {
            //Set line render end point to hitpoint
            lr.SetPosition(1,hitPoint);
        }    

        //Gets length of the line
        float length = (lr.GetPosition(1) - lr.GetPosition(0)).magnitude;

        float fadePoint = 1; //Holds where end of line should be (Color wise)
        float alphaEnd = 0.6f; //Holds end of line alpha value

        //If the line is greater than 4
        if (length > 4)
        {
            //Start incrementing both values down by 0.1 * how much longer length is from 4
            fadePoint = 1 - ((length - 4) * 0.1f);
            alphaEnd = 0.6f - ((length - 4) * 0.1f);
        }

        //Make sure fadePoint is never less than 0.1
        if (fadePoint < 0.1f)
        {
            fadePoint = 0.1f;
        }
        //Make sure alphaEnd is never less than 0
        if (alphaEnd < 0.0f)
        {
            alphaEnd = 0.0f;
        }

        //Gets new Gradients using values gained above
        gradient.SetKeys(
        new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.red, 1.0f) },
        new GradientAlphaKey[] { new GradientAlphaKey(0.6f, 0.0f), new GradientAlphaKey(alphaEnd, fadePoint) }
        );

        //Sets gradient to new gradient
        lr.colorGradient = gradient;

    }
}
