using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPop : MonoBehaviour
{
    //Gets popPrefab and where to put it
    public GameObject pop;
    public GameObject popsHolder;

    public float maxPops = 3; //Max pop's before death
    public float currentPops = 3; //Current pops left

    Transform pT;

    // Start is called before the first frame update
    void Start()
    {
        pT = popsHolder.GetComponent<Transform>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag != "SafeTouch")
        {
            //Increment pops
            currentPops--;

            //Gets collision point
            ContactPoint2D contact = collision.GetContact(0);

            //Turns collision normal into Quaternion
            Quaternion qNormal = Quaternion.LookRotation(new Vector3(0, 0, 1), contact.normal);

            //Creates pop object
            GameObject newPop = Instantiate(pop, contact.point, qNormal, pT);

            //Gives pop object push direction
            newPop.GetComponent<PopPush>().pushDirection = contact.normal;
        }
    }
}
