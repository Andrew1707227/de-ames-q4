using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class YEAHBOI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ChargeUnga;
    public GameObject Player;
    public float distance = 2.5f;
    public AIPath aiPath;
    Animator a;
    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Animator>();
        
        
        // Instantiate(UngaSpawn, SpawnToPlace.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 difference = Player.transform.position - gameObject.transform.position;
        if (difference.magnitude < distance)
        {
            aiPath.maxSpeed = 15;
            a.SetBool("charge", true);

        }
        else
        {
            a.SetBool("charge", false);
            aiPath.maxSpeed = 5;
        }
    }
}

