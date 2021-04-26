using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStop : MonoBehaviour
{
    public ParticleSystem ps;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" && collision.tag != "Trigger")
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<AudioSource>().Play();
            if (collision.tag == "Enemy") { //change this later
                var UnityVariablesAreDumb = ps.main; //it wont work unless i do this why unity - Andrew
                UnityVariablesAreDumb.startColor = new ParticleSystem.MinMaxGradient(new Color(0,1,0));
            }
            ps.Play();
            StartCoroutine(DestroyObject());
        }
    }
    private IEnumerator DestroyObject() {
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }
}
