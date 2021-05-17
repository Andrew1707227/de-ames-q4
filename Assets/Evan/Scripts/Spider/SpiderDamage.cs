using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderDamage : MonoBehaviour
{

    public int maxHealth = 15;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Bullets")
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                Debug.Log("Dead");
            }
            Debug.Log("Ow!");
        }
    }
}
