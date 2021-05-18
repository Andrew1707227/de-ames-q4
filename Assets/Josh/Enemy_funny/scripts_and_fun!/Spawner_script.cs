using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_script : MonoBehaviour
{
    public GameObject UngaSpawn;
    public GameObject SpawnToPlace;
    // Start is called before the first frame update
    
    void Start()
    {
        Instantiate(UngaSpawn, SpawnToPlace.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
