using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsMenu;

    //Gets brightness slider
    public GameObject brightSlider;

    Slider s;

    private void Start()
    {
        s = brightSlider.GetComponent<Slider>();

        //Sets slider to current brightOffset(No reset)
        s.value = BrightManager.brightnessShift;
    }

    // Update is called once per frame
    void Update()
    {
        //Updates brightnessShift
        BrightManager.brightnessShift = s.value;

    }

    public void toggleOptions()
    {
        //Toggle optionsMenu
        optionsMenu.SetActive(!optionsMenu.activeInHierarchy);
    }
}
