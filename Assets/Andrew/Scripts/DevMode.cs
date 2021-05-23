using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevMode : MonoBehaviour {

    public GameObject[] dialogue;
    public GameObject devHolder;
    private bool toggleAll;
    private bool toggleDamage;
    private bool toggleDialogue;
    public string SkipSceneName;

    void Start() {
        toggleDamage = false;
        toggleDialogue = false;
        toggleAll = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.BackQuote)) { 
            toggleAll = !toggleAll;
            devHolder.SetActive(toggleAll);
        }
    }

    public void SkipScene() {
        SceneManager.LoadScene(SkipSceneName);
    }

    public void DisableDialogue() {
        for (int i = 0; i < dialogue.Length; i++) {
            dialogue[i].SetActive(toggleDialogue);
        }
        toggleDialogue = !toggleDialogue;
    }

    public void NoDamage() {
        GameObject.Find("Player").GetComponent<PlayerPop>().popsDead = toggleDamage;
        toggleDamage = !toggleDamage;
    }

    public void ActivateInstantKill() {
        GameObject.Find("DamageBox").GetComponent<SpiderDamage>().currentHealth = 0;
        GameObject.Find("DamageBox (r)").GetComponent<SpiderDamage>().currentHealth = 0;
    }
}
