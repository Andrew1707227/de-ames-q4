using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditSceneManager : MonoBehaviour {

    public GameObject[] credits;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showCredits(GameObject credit) {
        for (int i = 0; i < credits.Length; i++) {
            credits[i].SetActive(false);
        }
        credit.SetActive(true);
    }

    public void menu() {
        SceneManager.LoadScene("TitleScene");
    }
}
