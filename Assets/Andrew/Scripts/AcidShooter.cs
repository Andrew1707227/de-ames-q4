using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidShooter : MonoBehaviour {
    public GameObject Acid;
    private Animator anim;
    public float fireRate;

    void Start() {
        anim = GetComponent<Animator>();
        StartCoroutine(AcidDropper());
    }
    
    private IEnumerator AcidDropper() {
        anim.enabled = true;
        yield return new WaitForSeconds(.25f);
        Instantiate(Acid,transform.position,Quaternion.Euler(0,0,0));
        yield return new WaitForSeconds(.25f);
        anim.enabled = false;
        yield return new WaitForSeconds(fireRate);
        StartCoroutine(AcidDropper());
    }
}
