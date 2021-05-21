using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Flippy : MonoBehaviour
{
    public AIPath aiPath;
    SpriteRenderer Sr;
    private void Start()
    {
       Sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            Sr.flipX = true;
            //transform.localScale = new Vector3(-1f, 1f, 1f);
        } else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            Sr.flipX = false;
           //transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
