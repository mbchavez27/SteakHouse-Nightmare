using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantDesignManager : MonoBehaviour
{
    public Sprite redrestaurant, bluerestaurant, whiterestaurant;


    void Update()
    {
        if (PlayerPrefs.GetString("restaurant") == "Default")
        {
            this.GetComponent<SpriteRenderer>().sprite = redrestaurant;
        }
        if (PlayerPrefs.GetString("restaurant") == "Blue")
        {
            this.GetComponent<SpriteRenderer>().sprite = bluerestaurant;
        }
        if (PlayerPrefs.GetString("restaurant") == "White")
        {
            this.GetComponent<SpriteRenderer>().sprite = whiterestaurant;
        }
    }
}
