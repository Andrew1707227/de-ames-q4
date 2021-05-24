using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Flippy : MonoBehaviour
{
    public AIPath aiPath;
    SpriteRenderer Sr;

    PolygonCollider2D pCRight;
    PolygonCollider2D pCLeft;

    private void Start()
    {
       Sr = GetComponent<SpriteRenderer>();

       pCRight = GetComponents<PolygonCollider2D>()[0];
       pCLeft = GetComponents<PolygonCollider2D>()[1];
    }

    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {

            pCRight.enabled = !pCRight.enabled;
            pCLeft.enabled = !pCLeft.enabled;

            Sr.flipX = true;
            //transform.localScale = new Vector3(-1f, 1f, 1f);
        } else if (aiPath.desiredVelocity.x <= -0.01f)
        {

            pCRight.enabled = !pCRight.enabled;
            pCLeft.enabled = !pCLeft.enabled;

            Sr.flipX = false;
           //transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
