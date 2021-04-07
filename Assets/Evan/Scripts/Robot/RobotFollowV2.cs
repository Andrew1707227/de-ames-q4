using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFollowV2 : MonoBehaviour
{
    //Holds movement speed
    [SerializeField] float speed = 6;

    [SerializeField] GameObject player; //Holds player
    Transform playerTr; //Holds player transform

    public Vector3 baseOffset = new Vector3(0.5f, 0.6f, 0); //Holds default position offset 
    public Vector3 offset; //Holds position offset
    public Vector3 currentOffset; //Holds poition offset to doudge walls

    bool hit = false;

    Vector3 playerPos; //Holds player position

    Rigidbody2D rb2;

    // Start is called before the first frame update
    void Start()
    {
        playerTr = player.GetComponent<Transform>();
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        offset = baseOffset;
        currentOffset = offset;
    }

    // Update is called once per frame
    void Update()
    {
        //Gets player position
        playerPos = playerTr.position;

        Vector3 distance = (playerPos + offset) - transform.position;
        Vector3 currentDistance;

        int layerMask =~ LayerMask.GetMask("Player");

        if (!hit)
        {
            RaycastHit2D obstaclesFinder = Physics2D.Raycast(gameObject.transform.position, distance, distance.magnitude, layerMask);
            Debug.DrawRay(gameObject.transform.position, distance, Color.white);
            if (obstaclesFinder.point != Vector2.zero)
            {
                hit = true;
                currentOffset = Vector3.zero;
            }
            else
            {
                currentOffset = offset;
            }
        }
        else
        {
            Vector3 tempDistance = (playerPos + offset) - playerPos;
            RaycastHit2D obstaclesFinder = Physics2D.Raycast(playerPos, tempDistance, tempDistance.magnitude, layerMask);
            Debug.DrawRay(playerPos, tempDistance, Color.red);
            if (obstaclesFinder.point != Vector2.zero)
            {
                currentOffset = Vector3.zero;
            }
            else
            {
                hit = false;
                currentOffset = offset;
            }
        }

        currentDistance = (playerPos + currentOffset) - transform.position;

        //If distance is long
        if (currentDistance.magnitude > 0.01f)
        {
            //Move towards player fast
            rb2.velocity = currentDistance * speed;
        }
        else //If distance is short
        {
            //Move towards player precisely
            rb2.velocity = currentDistance;
        }  
    } 
}
