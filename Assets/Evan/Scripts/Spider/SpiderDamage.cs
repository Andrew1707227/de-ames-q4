using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderDamage : MonoBehaviour
{

    public GameObject healthBar;
    public GameObject otherDamageBox;

    public int maxHealth = 15;
    [HideInInspector]
    public int currentHealth;

    SpiderDamage sd;
    Slider s;

    // Start is called before the first frame update
    void Start()
    {
        sd = otherDamageBox.GetComponent<SpiderDamage>();
        s = healthBar.GetComponent<Slider>();
        s.maxValue = maxHealth;
        s.value = maxHealth;

        currentHealth = maxHealth;
        sd.currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth < s.value)
        {
            s.value -= 0.05f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Bullets")
        {
            currentHealth--;
            sd.currentHealth--;

            if (currentHealth <= 0)
            {
                Debug.Log("Dead");
            }
        }
    }
}
