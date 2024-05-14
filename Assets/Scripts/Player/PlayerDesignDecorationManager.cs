using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDesignDecorationManager : MonoBehaviour
{

    public Sprite WhiteChef,BlackChef,RedChef;

    void Update()
    {
        if(PlayerPrefs.GetString("Chef") == "Default")
        {
            this.GetComponent<SpriteRenderer>().sprite = WhiteChef;
        }
        if (PlayerPrefs.GetString("Chef") == "Black")
        {
            this.GetComponent<SpriteRenderer>().sprite = BlackChef;
        }
        if (PlayerPrefs.GetString("Chef") == "Red")
        {
            this.GetComponent<SpriteRenderer>().sprite = RedChef;
        }
    }
}
