using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    public GameObject targetLegBase;
    public GameObject spiderBody;

    public float footOffset = -1.83f;
    public float pXLeeway = 1.9f;
    public float nXLeeway = 2.3f;
    float wantedX;

    int layerMask;

    bool CR_running = false;
    IEnumerator coroutine;

    Transform lt;
    Rigidbody2D rb2;

    // Start is called before the first frame update
    void Start()
    {
        lt = targetLegBase.GetComponent<Transform>();
        rb2 = spiderBody.GetComponent<Rigidbody2D>();

        //Get layermask
        layerMask = ~LayerMask.GetMask("Spider");
    }

    // Update is called once per frame
    void Update()
    {
        wantedX = lt.position.x + footOffset;

        if (transform.position.x > wantedX + pXLeeway) 
        {
            RaycastHit2D wallChecker = Physics2D.Raycast(lt.position, -Vector2.right, 2, layerMask);
            Debug.DrawRay(lt.position, new Vector3(-2, 0, 0), Color.red, 3);

            if (wallChecker.point != Vector2.zero)
            {
                if (CR_running)
                {
                    StopCoroutine(coroutine);
                    CR_running = false;
                }
                coroutine = arcMove(wallChecker.point);
                StartCoroutine(coroutine);
            }
            else
            {
                RaycastHit2D floorChecker = Physics2D.Raycast(new Vector2(wantedX, lt.position.y), Vector2.down, 2, layerMask);
                Debug.DrawRay(new Vector2(wantedX, lt.position.y), new Vector3(0, -2, 0), Color.red, 3);

                if (CR_running)
                {
                    StopCoroutine(coroutine);
                    CR_running = false;
                }
                coroutine = arcMove(floorChecker.point);
                StartCoroutine(coroutine);
            }
        }
        

        if (transform.position.x < wantedX - nXLeeway)
        {
            RaycastHit2D wallChecker = Physics2D.Raycast(lt.position, Vector2.right, 2f, layerMask);
            Debug.DrawRay(lt.position, new Vector3(2f, 0, 0), Color.blue, 3);

            if (wallChecker.point != Vector2.zero)
            {
                if (CR_running)
                {
                    StopCoroutine(coroutine);
                    CR_running = false;
                }
                coroutine = arcMove(wallChecker.point);
                StartCoroutine(coroutine);
            }
            else
            {
                RaycastHit2D floorChecker = Physics2D.Raycast(new Vector2(wantedX, lt.position.y), Vector2.down, 2f, layerMask);
                Debug.DrawRay(new Vector2(wantedX, lt.position.y), new Vector3(0, -2, 0), Color.blue, 3);

                if (CR_running)
                {
                    StopCoroutine(coroutine);
                    CR_running = false;
                }
                coroutine = arcMove(floorChecker.point);
                StartCoroutine(coroutine);
            }
        }
    }

    private IEnumerator arcMove(Vector3 endPoint)
    {
        CR_running = true;

        Vector3 difference = endPoint - transform.position;

        int times = 25;

        for(int i = 0; i < times; i++)
        {
            transform.position += difference / times;

            if (i <= times / 2)
            {
                transform.position += new Vector3(0, 0.05f, 0);
            }
            else
            {
                transform.position -= new Vector3(0, 0.05f, 0);
            }

            //Will need to change from rb2.velocity.x when start turning spider
            yield return new WaitForSeconds(0.0025f / rb2.velocity.x); //Wait
        }
        CR_running =false;
    }
}
