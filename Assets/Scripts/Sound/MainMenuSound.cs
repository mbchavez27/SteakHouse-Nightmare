using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuSound : MonoBehaviour
{
    public Text soundtext;
    string sound;  


    void Update()
    {
        Debug.Log(PlayerPrefs.GetString("sound",sound));
        if(PlayerPrefs.GetString("sound",sound) == "on")
        {
            soundtext.text = "Sound On";
        }
        if (PlayerPrefs.GetString("sound", sound) == "off")
        {
            soundtext.text = "Sound Off";
        }
        if (PlayerPrefs.GetString("sound", sound) == null)
        {
            soundtext.text = "Sound Onn";
        }
    }

    public void ChangeSound()
    {
        if (PlayerPrefs.GetString("sound", sound) == "on" || PlayerPrefs.GetString("sound", sound) == null)
        {
            sound = "off";
            PlayerPrefs.SetString("sound", sound);
        }
        else
        {
            sound = "on";
            PlayerPrefs.SetString("sound", sound);
        }
    }

}
