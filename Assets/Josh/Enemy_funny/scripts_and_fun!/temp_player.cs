using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp_player : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        gameObject.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void KillPlayer()
    {
        Destroy(this.gameObject);
    }
}
