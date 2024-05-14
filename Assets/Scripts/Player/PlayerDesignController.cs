using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDesignController : MonoBehaviour
{

    public Sprite[] WhiteChef = new Sprite[8];
    public Sprite[] BlackChef = new Sprite[8];
    public Sprite[] RedChef = new Sprite[8];
    
    void Update()
    {
        if (PlayerPrefs.GetString("Chef") == "Default")
        {
            this.GetComponent<PlayerController>().playerSprites[0] = WhiteChef[0];
            this.GetComponent<PlayerController>().playerSprites[1] = WhiteChef[1];
            this.GetComponent<PlayerController>().playerSprites[2] = WhiteChef[2];
            this.GetComponent<PlayerController>().playerSprites[3] = WhiteChef[3];
            this.GetComponent<PlayerController>().playerSprites[4] = WhiteChef[4];
            this.GetComponent<PlayerController>().playerSprites[5] = WhiteChef[5];
            this.GetComponent<PlayerController>().playerSprites[6] = WhiteChef[6];
            this.GetComponent<PlayerController>().playerSprites[7] = WhiteChef[7];
        }
        if (PlayerPrefs.GetString("Chef") == "Black")
        {
            this.GetComponent<PlayerController>().playerSprites[0] = BlackChef[0];
            this.GetComponent<PlayerController>().playerSprites[1] = BlackChef[1];
            this.GetComponent<PlayerController>().playerSprites[2] = BlackChef[2];
            this.GetComponent<PlayerController>().playerSprites[3] = BlackChef[3];
            this.GetComponent<PlayerController>().playerSprites[4] = BlackChef[4];
            this.GetComponent<PlayerController>().playerSprites[5] = BlackChef[5];
            this.GetComponent<PlayerController>().playerSprites[6] = BlackChef[6];
            this.GetComponent<PlayerController>().playerSprites[7] = BlackChef[7];
        }
        if (PlayerPrefs.GetString("Chef") == "Red")
        {
            this.GetComponent<PlayerController>().playerSprites[0] = RedChef[0];
            this.GetComponent<PlayerController>().playerSprites[1] = RedChef[1];
            this.GetComponent<PlayerController>().playerSprites[2] = RedChef[2];
            this.GetComponent<PlayerController>().playerSprites[3] = RedChef[3];
            this.GetComponent<PlayerController>().playerSprites[4] = RedChef[4];
            this.GetComponent<PlayerController>().playerSprites[5] = RedChef[5];
            this.GetComponent<PlayerController>().playerSprites[6] = RedChef[6];
            this.GetComponent<PlayerController>().playerSprites[7] = RedChef[7];
        }
    }
}
