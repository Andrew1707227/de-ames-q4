using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopPush : MonoBehaviour
{
    //Holds player and arm names to find them as a prefab
    public string playerName;
    public string armName;

    //Holds direction to push player
    public Vector3 pushDirection;

    Rigidbody2D pRB2;
    Flipper aF;
    ParticleSystem cPS;

    // Start is called before the first frame update
    void Start()
    {
        pRB2 = GameObject.Find(playerName).GetComponent<Rigidbody2D>();
        aF = GameObject.Find(armName).GetComponent<Flipper>();
        cPS = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //Pushes player
        pRB2.AddForce(pushDirection/2 * Time.deltaTime, ForceMode2D.Impulse);

        //If bool from flipper says to flip
        if (aF.Popflipx == true)
        {
            //Flip x in push direction
            pushDirection = new Vector3(-Mathf.Abs(pushDirection.x),pushDirection.y , pushDirection.z);

            //Preform black magic I found on the internet to change the shape module and tunr the particles around
            var newShape = cPS.shape;
            newShape.rotation = new Vector3(newShape.rotation.x, Mathf.Abs(newShape.rotation.y), newShape.rotation.z);

        }
        else
        {
            //Flip x in push direction
            pushDirection = new Vector3(Mathf.Abs(pushDirection.x), pushDirection.y, pushDirection.z);

            //Preform black magic I found on the internet to change the shape module and tunr the particles around
            var newShape = cPS.shape;
            newShape.rotation = new Vector3(newShape.rotation.x, -Mathf.Abs(newShape.rotation.y), newShape.rotation.z);
        }
    }
}
