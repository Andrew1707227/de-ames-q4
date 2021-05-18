using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Flippy : MonoBehaviour
{
    public AIPath aiPath;

    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {if (transform.rotation.x == 0)
            {
                transform.Rotate(new Vector3(0, 180, 0));
            }

            
        }else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            if(transform.rotation.x == 180)
            {
                transform.Rotate(new Vector3(0, -180, 0));
            }
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
