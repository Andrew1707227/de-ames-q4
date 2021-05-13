using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegRear : MonoBehaviour
{
    public GameObject spiderBody;
    public Vector3 legChange;

    TargetMove tm;
    Transform sbT;

    // Start is called before the first frame update
    void Start()
    {
        tm = gameObject.GetComponent<TargetMove>();
        sbT = spiderBody.GetComponent<Transform>();
    }

    public void doRearUp()
    {
        StartCoroutine(rearUp());
    }

    private IEnumerator rearUp()
    {
        tm.enabled = false;

        int times = 10;
        Vector3 localLegChange = new Vector3(-sbT.InverseTransformDirection(legChange).x, sbT.InverseTransformDirection(legChange).y, 0);

        for (int j = 0; j < times; j++)
        {
            transform.position += localLegChange / times;
            yield return new WaitForFixedUpdate();
        }

        //ftT[i].position += legChange;
        //yield return new WaitForFixedUpdate();      

        //tm.enabled = true;
    }
}
