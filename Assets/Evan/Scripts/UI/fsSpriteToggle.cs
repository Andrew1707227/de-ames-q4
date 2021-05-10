using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class fsSpriteToggle : MonoBehaviour
{
    public Sprite checkedBox;
    public Sprite uncheckedBox;

    Image i;

    // Start is called before the first frame update
    void Start()
    {
        i = gameObject.GetComponent<Image>();
    }

    public void toggleSprite()
    {
        if (i.sprite == checkedBox)
        {
            i.sprite = uncheckedBox;
        }
        else
        {
            i.sprite = checkedBox;
        }
    }
}
