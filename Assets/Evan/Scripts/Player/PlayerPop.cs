using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPop : MonoBehaviour
{

    public GameObject pop;
    public GameObject popsHolder;

    Transform pT;

    // Start is called before the first frame update
    void Start()
    {
        pT = popsHolder.GetComponent<Transform>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);

        Quaternion qNormal = Quaternion.LookRotation(new Vector3(0, 0, 1), contact.normal);

        GameObject newPop = Instantiate(pop, contact.point, qNormal, pT);

        newPop.GetComponent<PopPush>().pushDirection = contact.normal;
    }
}
