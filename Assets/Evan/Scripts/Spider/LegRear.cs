using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegRear : MonoBehaviour
{
    public GameObject rearUpPoint;

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

    private IEnumerator rearUp()
    {
        tm.enabled = false;

        int times = 10;
        Vector3 difference = rpT.position - transform.position;

        for (int j = 0; j < times; j++)
        {
            transform.position += difference / times;
            yield return new WaitForFixedUpdate();
        }

        //ftT[i].position += legChange;
        //yield return new WaitForFixedUpdate();      

        //tm.enabled = true;
    }
}
