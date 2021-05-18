using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIntro : MonoBehaviour
{

    public GameObject player;
    public GameObject spiderBody;
    public GameObject introSpiderBody;
    public Camera mainCamera;

    Transform pT;

    // Start is called before the first frame update
    void Start()
    {
        pT = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
