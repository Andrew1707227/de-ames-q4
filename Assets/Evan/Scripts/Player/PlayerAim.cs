using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    //Reference to main Camera
    public Camera cam;

    //holds mouse position
    Vector2 mousePos;

    //Holds the current look direction and sets the starting vector for it
    public Vector2 currentLookDir = new Vector2(1, 0);

    // Update is called once per frame
    void Update()
    {
        //Sets mousePos to current mouse position
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    //Physics code, fixed update is called before physics updates
    void FixedUpdate()
    {
        //Gets the previous look direction by setting it to the last instance of currentLookDir
        Vector2 previousLookDir = currentLookDir;

        //Gets a vector from the turret pointing towards the mouse
        currentLookDir = mousePos - new Vector2(transform.parent.position.x, transform.parent.position.y);

        //Turn 90 degress becuase its always off by that much (idk why)
        currentLookDir = new Vector2(-currentLookDir.y, currentLookDir.x);

        //Gets the angle change by comparing previousLookDir and currentLookDir in the SignedAngle method
        float angle = Vector2.SignedAngle(previousLookDir, currentLookDir);

        if ((currentLookDir.x > 0.1f || currentLookDir.x < 0.1f) && (currentLookDir.y > 0.1f || currentLookDir.y < 0.1f))
        {
            //Rotates by angle around the center of the turret's parent
            transform.RotateAround(new Vector3(transform.parent.position.x, transform.parent.position.y, 0), new Vector3(0, 0, 1), angle);
        }
    }
}
