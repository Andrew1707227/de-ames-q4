using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopPush : MonoBehaviour
{
    public string playerName;

    public Vector3 pushDirection;

    Rigidbody2D pRB2;

    // Start is called before the first frame update
    void Start()
    {
        pRB2 = GameObject.Find(playerName).GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        pRB2.AddForce(pushDirection/2 * Time.deltaTime, ForceMode2D.Impulse);
    }
}
