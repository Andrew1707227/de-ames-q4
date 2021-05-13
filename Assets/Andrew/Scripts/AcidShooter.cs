using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidShooter : MonoBehaviour {
    public GameObject Acid;
    private Animator anim;
    public float fireRate;
    [Tooltip("Whether or not the acid should fire up to the ceiling.")]
    public bool isUp;

    void Start() {
        anim = GetComponent<Animator>();
        StartCoroutine(AcidDropper());
    }
    
    private IEnumerator AcidDropper() {
        anim.enabled = true;
        yield return new WaitForSeconds(.25f);
        GameObject acidClone = Instantiate(Acid,transform.position,Quaternion.Euler(0,0,0));
        if (isUp) {
            acidClone.GetComponent<Rigidbody2D>().gravityScale = -1;
        }
        yield return new WaitForSeconds(.25f);
        anim.enabled = false;
        yield return new WaitForSeconds(fireRate);
        StartCoroutine(AcidDropper());
    }
}
