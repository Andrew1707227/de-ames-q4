using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChecpointGet : MonoBehaviour
{
    public bool activated = false;
    public string playerName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !activated)
        {
            activated = true;
            GameObject.Find(playerName).GetComponent<PlayerPop>().checkpoint = transform.position;
        }
    }
}
