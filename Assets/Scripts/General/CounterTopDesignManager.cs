using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterTopDesignManager : MonoBehaviour
{
    public Sprite  browntop, graytop;
    public Sprite  brownsep, graysep;
    public Sprite  browntopdown, graytopdown;
    public bool isSeperator = false;
    public bool isTopDown = false;

    void Update()
    {
        if (PlayerPrefs.GetString("restaurant") == "Default")
        {
            if (!isSeperator && !isTopDown)
            {
                this.GetComponent<SpriteRenderer>().sprite = browntop;
            }
            if (isSeperator)
            {
                this.GetComponent<SpriteRenderer>().sprite = brownsep;
            }
            if (isTopDown)
            {
                this.GetComponent<SpriteRenderer>().sprite = browntopdown;
            }
        }
        if (PlayerPrefs.GetString("restaurant") == "Blue")
        {
            if (!isSeperator && !isTopDown)
            {
                this.GetComponent<SpriteRenderer>().sprite = graytop;
            }
            if (isSeperator)
            {
                this.GetComponent<SpriteRenderer>().sprite = graysep;
            }
            if (isTopDown)
            {
                this.GetComponent<SpriteRenderer>().sprite = graytopdown;
            }
        }
        if (PlayerPrefs.GetString("restaurant") == "White")
        {
            if (!isSeperator && !isTopDown)
            {
                this.GetComponent<SpriteRenderer>().sprite = graytop;
            }
            if (isSeperator)
            {
                this.GetComponent<SpriteRenderer>().sprite = graysep;
            }
            if (isTopDown)
            {
                this.GetComponent<SpriteRenderer>().sprite = graytopdown;
            }
        }
    }
}
