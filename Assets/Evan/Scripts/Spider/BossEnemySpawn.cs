using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemySpawn : MonoBehaviour
{
    public GameObject player;
    public GameObject damageBox;

    public GameObject UngaSpawn;

    public float spawnDelay = 20;
    float currentSD = 0;

    float maxBossHealth;
    float bossHealth;

    Transform t;
    SpiderDamage sd;
    Transform[] spawns = new Transform[8];

    // Start is called before the first frame update
    void Start()
    {
        t = player.GetComponent<Transform>();
        sd = damageBox.GetComponent<SpiderDamage>();

        for (int i = 0; i < 8; i++)
        {
            spawns[i] = transform.GetComponentsInChildren<Transform>()[i];
        }

        maxBossHealth = sd.maxHealth;
        bossHealth = sd.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        bossHealth = sd.currentHealth;

        if (bossHealth <= maxBossHealth / 2 && bossHealth != 0 && currentSD <= 0)
        {
            currentSD = spawnDelay;

            Vector3 difference = spawns[0].position - t.position;
            int farthest = 0;

            for (int i = 1; i < spawns.Length; i++)
            {
                if ((spawns[i].position - t.position).magnitude > difference.magnitude)
                {
                    difference = spawns[i].position - t.position;
                    farthest = i;
                }
            }

            GameObject clone = Instantiate(UngaSpawn, spawns[farthest].position, Quaternion.identity);
            clone.SetActive(true);
        }
        else
        {
            currentSD -= Time.deltaTime; 
        }
    }
}
