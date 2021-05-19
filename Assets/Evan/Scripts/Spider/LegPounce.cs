using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegPounce : MonoBehaviour
{
    //Vector3 oldPos;

    public GameObject miniRearPoint;
    public GameObject spiderBody;

    //int layerMask;
    public LayerMask layerMask;
    public LayerMask noPLayerMask;

    Transform mpT;
    TargetMove tm;
    SpiderSpinner ss;
    Attacks a;

    // Start is called before the first frame update
    void Start()
    {
        mpT = miniRearPoint.GetComponent<Transform>();
        tm = gameObject.GetComponent<TargetMove>();
        ss = spiderBody.GetComponent<SpiderSpinner>();
        a = spiderBody.GetComponent<Attacks>();

        //layerMask = ~LayerMask.GetMask("Spider");
    }

    public void doLegPounce(bool isFront, Vector3 isDifferemce, Vector3 isLift)
    {
        StartCoroutine(legPounce(isFront, isDifferemce, isLift));
    }

    private IEnumerator legPounce(bool front, Vector3 difference, Vector3 lift)
    {
        tm.enabled = false;

        if (front)
        {
            int times = 15;
            Vector3 rearDifference = mpT.position - transform.position;

            for (int j = 0; j < times; j++)
            {
                transform.position += rearDifference / times;
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            int times = 15;
            for (int j = 0; j < times; j++)
            {
                yield return new WaitForFixedUpdate();
            }
        }

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        yield return new WaitForSeconds(1f);

        /*
        for (int i = 0; i < 35; i++)
        {
            transform.position += difference / 35;

            if (i <= 35 / 2)
            {
                transform.position += lift;
            }
            else
            {
                transform.position -= lift;
            }
            yield return new WaitForFixedUpdate();
        }

        tm.enabled = true;
        */


        if (!front)
        {
            for (int j = 0; j < 4; j++)
            {
                yield return new WaitForFixedUpdate();
            }

            for (int i = 0; i < 16; i++)
            {
                transform.position += difference / 35;

                transform.position += lift;

                yield return new WaitForFixedUpdate();
            }

            int k = 17;
            RaycastHit2D floorDistance = Physics2D.Raycast(transform.position, -transform.up, 10, layerMask);
            while (floorDistance.distance > 1.1f)
            {
                if (k > 35)
                {
                    tm.enabled = true;
                }

                transform.position += difference / 35;

                transform.position -= lift;


                //Checks grond distance on both ends
                RaycastHit2D leftDistance = Physics2D.Raycast(ss.lT.position, -transform.up, 20, layerMask);
                RaycastHit2D rightDistance = Physics2D.Raycast(ss.rT.position, -transform.up, 20, layerMask);

                //If distance uneven enough rotate until fixed
                if (Mathf.Round(leftDistance.distance * 10f) / 10f > Mathf.Round(rightDistance.distance * 10f) / 10f)
                {
                    transform.Rotate(new Vector3(0, 0, 0.75f));
                }
                else if (Mathf.Round(leftDistance.distance * 10f) / 10f < Mathf.Round(rightDistance.distance * 10f) / 10f)
                {
                    transform.Rotate(new Vector3(0, 0, -0.75f));
                }

                k++;
                floorDistance = Physics2D.Raycast(transform.position, -transform.up, 10, layerMask);
                yield return new WaitForFixedUpdate();
            }

            tm.enabled = true;
            //tm.toGround();
        }
        else
        {

            for (int i = 0; i < 16; i++)
            {
                transform.position += difference / 35;

                transform.position += lift;

                yield return new WaitForFixedUpdate();
            }

            RaycastHit2D floorDistance = Physics2D.Raycast(transform.position, -transform.up, 10, noPLayerMask);
            while (floorDistance.distance > 1.1f && !a.startedRear)
            {

                transform.position += difference / 35;

                transform.position -= lift;


                //Checks grond distance on both ends
                RaycastHit2D leftDistance = Physics2D.Raycast(ss.lT.position, -transform.up, 20, layerMask);
                RaycastHit2D rightDistance = Physics2D.Raycast(ss.rT.position, -transform.up, 20, layerMask);

                //If distance uneven enough rotate until fixed
                if (Mathf.Round(leftDistance.distance * 10f) / 10f > Mathf.Round(rightDistance.distance * 10f) / 10f)
                {
                    transform.Rotate(new Vector3(0, 0, 0.75f));
                }
                else if (Mathf.Round(leftDistance.distance * 10f) / 10f < Mathf.Round(rightDistance.distance * 10f) / 10f)
                {
                    transform.Rotate(new Vector3(0, 0, -0.75f));
                }

                floorDistance = Physics2D.Raycast(transform.position, -transform.up, 10, noPLayerMask);
                yield return new WaitForFixedUpdate();
            }

            /*
            for (int i = 0; i < 35; i++)
            {
                transform.position += difference / 35;

                if (i <= 35 / 2)
                {
                    transform.position += lift;
                }
                else
                {
                    transform.position -= lift;
                }
                yield return new WaitForFixedUpdate();
            }
            */
        }
    }
}
