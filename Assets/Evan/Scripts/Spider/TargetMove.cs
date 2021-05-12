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
    float localWantedX;
    float wantedX;

    int layerMask;

    bool CR_running = false;
    IEnumerator coroutine;

    Transform st;
    Transform lt;
    SpiderFollow sf;
    Rigidbody2D rb2;

    // Start is called before the first frame update
    void Start()
    {
        st = spiderBody.GetComponent<Transform>();
        lt = targetLegBase.GetComponent<Transform>();
        sf = spiderBody.GetComponent<SpiderFollow>();
        rb2 = spiderBody.GetComponent<Rigidbody2D>();

        //Get layermask
        layerMask = ~LayerMask.GetMask("Spider");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localLimbStart = st.InverseTransformPoint(lt.position);
        Vector3 localPosition = st.InverseTransformPoint(transform.position);

        float curFootOffset;
        float curPXLeeway;
        float curNXLeeway;
        if (sf.flipped)
        {
            curFootOffset = -footOffset;
            curPXLeeway = -pXLeeway;
            curNXLeeway = -nXLeeway;
        }
        else
        {
            curFootOffset = footOffset;
            curPXLeeway = pXLeeway;
            curNXLeeway = nXLeeway;
        }

        localWantedX = st.InverseTransformPoint(lt.position).x + curFootOffset;
        wantedX = lt.position.x + curFootOffset;

        if (localPosition.x > localWantedX + pXLeeway) 
        {
            /*
            RaycastHit2D wallChecker = Physics2D.Raycast(lt.position, -st.right, 4, layerMask);
            Debug.DrawRay(lt.position, -st.right, Color.red, 3);

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
            */
                Vector2 floorCheckStart = st.TransformPoint(new Vector2(localWantedX, localLimbStart.y));
                RaycastHit2D floorChecker = Physics2D.Raycast(floorCheckStart, -st.up, 4, layerMask);
                Debug.DrawRay(floorCheckStart, -st.up, Color.red, 3);

                if (CR_running)
                {
                    StopCoroutine(coroutine);
                    CR_running = false;
                }
                coroutine = arcMove(floorChecker.point);
                StartCoroutine(coroutine);
            //}
        }
        

        if (localPosition.x < localWantedX - nXLeeway)
        {
            /*
            RaycastHit2D wallChecker = Physics2D.Raycast(lt.position, st.right, 4f, layerMask);
            Debug.DrawRay(lt.position, st.right, Color.blue, 3);

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
            */
                Vector2 floorCheckStart2 = st.TransformPoint(new Vector2(localWantedX, localLimbStart.y));
                RaycastHit2D floorChecker = Physics2D.Raycast(floorCheckStart2, -st.up, 4f, layerMask);
                Debug.DrawRay(floorCheckStart2, -st.up, Color.blue, 3);

                if (CR_running)
                {
                    StopCoroutine(coroutine);
                    CR_running = false;
                }
                coroutine = arcMove(floorChecker.point);
                StartCoroutine(coroutine);
            //}
        }
    }

    private IEnumerator arcMove(Vector3 endPoint)
    {
        CR_running = true;

        Vector3 difference = endPoint - transform.position;
        Vector3 lift = new Vector3(-st.InverseTransformDirection(new Vector3(0, 0.05f, 0)).x, st.InverseTransformDirection(new Vector3(0, 0.05f, 0)).y, 0);

        int times = 25;

        for(int i = 0; i < times; i++)
        {
           transform.position += difference / times;

            if (i <= times / 2)
            {
                transform.position += lift;
            }
            else
            {
                transform.position -= lift;
            }
            yield return new WaitForSeconds(0.0025f / st.InverseTransformDirection(rb2.velocity).x); //Wait
        }
        CR_running =false;
    }
}
