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

    Transform st;
    Transform lt;
    Rigidbody2D rb2;

    // Start is called before the first frame update
    void Start()
    {
        st = spiderBody.GetComponent<Transform>();
        lt = targetLegBase.GetComponent<Transform>();
        rb2 = spiderBody.GetComponent<Rigidbody2D>();

        //Get layermask
        layerMask = ~LayerMask.GetMask("Spider");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localLimbStart = st.InverseTransformPoint(lt.position);
        Vector3 localPosition = st.InverseTransformPoint(transform.position);

        wantedX = st.InverseTransformPoint(lt.position).x + footOffset;

        Debug.Log(localPosition);

        if (localPosition.x > wantedX + pXLeeway) 
        {
            RaycastHit2D wallChecker = Physics2D.Raycast(localLimbStart, -st.right, 2, layerMask);
            Debug.DrawRay(localLimbStart, -st.right, Color.red, 3);

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
                RaycastHit2D floorChecker = Physics2D.Raycast(new Vector2(wantedX, localLimbStart.y), -st.up, 2, layerMask);
                Debug.DrawRay(new Vector2(wantedX, localLimbStart.y), -st.up, Color.red, 3);

                if (CR_running)
                {
                    StopCoroutine(coroutine);
                    CR_running = false;
                }
                coroutine = arcMove(floorChecker.point);
                StartCoroutine(coroutine);
            }
        }
        

        if (localPosition.x < wantedX - nXLeeway)
        {
            RaycastHit2D wallChecker = Physics2D.Raycast(localLimbStart, st.right, 2f, layerMask);
            Debug.DrawRay(localLimbStart, st.right, Color.blue, 3);

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
                RaycastHit2D floorChecker = Physics2D.Raycast(new Vector2(wantedX, localLimbStart.y), -st.up, 2f, layerMask);
                Debug.DrawRay(new Vector2(wantedX, localLimbStart.y), -st.up, Color.blue, 3);

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

            yield return new WaitForSeconds(0.0025f / transform.InverseTransformDirection(rb2.velocity).x); //Wait
        }
        CR_running =false;
    }
}
