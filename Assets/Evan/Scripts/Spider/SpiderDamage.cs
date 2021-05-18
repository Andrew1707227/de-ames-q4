using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderDamage : MonoBehaviour
{

    public GameObject healthBar;
    public GameObject otherDamageBox;
    public GameObject spiderBody;
    public GameObject Endcutscene;
    public GameObject player;
    public Image Fade;

    public int maxHealth = 15;
    [HideInInspector]
    public int currentHealth;

    bool didDeath = false;

    SpiderDamage sd;
    Slider s;
    Image ei;

    // Start is called before the first frame update
    void Start()
    {
        sd = otherDamageBox.GetComponent<SpiderDamage>();
        s = healthBar.GetComponent<Slider>();
        ei = Endcutscene.GetComponent<Image>();
        s.maxValue = maxHealth;
        s.value = 1;// maxHealth;

        currentHealth = 1;// maxHealth;
        sd.currentHealth = 1;// maxHealth;
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

            if (currentHealth <= 0 && !didDeath)
            {
                spiderBody.GetComponent<SpiderFollow>().dead = true;
                player.GetComponent<PlayerPop>().popsDead = true;
                spiderBody.GetComponent<Attacks>().enabled = false;

                //Pause sound
                AudioListener.pause = true;

                StartCoroutine(FadeToWhite());

                didDeath = true;
            }
        }
    }

    private IEnumerator FadeToWhite()
    {
        Color temp = Color.white;
        for (float i = 0; i <= 1; i += 1 / 120f)
        {
            Fade.color = new Color(temp.r, temp.g, temp.b, i);
            yield return new WaitForFixedUpdate();
        }
        Fade.color = new Color(temp.r, temp.g, temp.b, 1);
        PlayerMoveV2.prevSpeed = GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity;

        for (float i = 0; i <= 1; i += 1 / 120f)
        {
            ei.color = new Color(ei.color.r, ei.color.g, ei.color.b, i);
            yield return new WaitForFixedUpdate();
        }

        Endcutscene.GetComponent<Animator>().enabled = true;
        //SceneManager.LoadScene(SceneName);
    }
}
