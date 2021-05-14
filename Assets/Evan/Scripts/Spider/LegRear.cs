using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegRear : MonoBehaviour
{
    public GameObject rearUpPoint;

    Vector3 oldPos;

    TargetMove tm;
    Transform rpT;

    // Start is called before the first frame update
    void Start()
    {
        tm = gameObject.GetComponent<TargetMove>();
        rpT = rearUpPoint.GetComponent<Transform>();
    }

    public void doRearUp()
    {
        StartCoroutine(rearUp());
    }

    public void doRearDown()
    {
        StartCoroutine(rearDown());
    }

    private IEnumerator rearUp()
    {
        tm.enabled = false;

        oldPos = transform.position;

        int times = 15;
        Vector3 difference = rpT.position - transform.position;

        for (int j = 0; j < times; j++)
        {
            transform.position += difference / times;
            yield return new WaitForFixedUpdate();
        }    
    }

    private IEnumerator rearDown()
    {
        int times = 15;
        Vector3 difference = oldPos - transform.position;

        for (int j = 0; j < times; j++)
        {
            transform.position += difference / times;
            yield return new WaitForFixedUpdate();
        }

        tm.enabled = true;
    }
}
