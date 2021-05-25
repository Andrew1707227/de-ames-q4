using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_script : MonoBehaviour
{
    public GameObject UngaSpawn;
    public GameObject Player;
    public GameObject SpawnToPlace;

    public bool Stop = false;
    public float distance = 5;
    // Start is called before the first frame update
    void Start()
    {

        Stop = false;
        // Instantiate(UngaSpawn, SpawnToPlace.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Player.transform.position - gameObject.transform.position;
        if (difference.magnitude < distance && !Stop)
        {
            Stop = true;
            GetComponent<AudioSource>().Play();
            GameObject clone = Instantiate(UngaSpawn, SpawnToPlace.transform.position, Quaternion.identity);
            clone.SetActive(true);
        }
    }
}
