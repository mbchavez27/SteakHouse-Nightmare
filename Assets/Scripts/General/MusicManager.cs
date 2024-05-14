using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MusicManager : MonoBehaviour
{
    public AudioSource audioSrc,audiocook;
    public AudioClip pickup, throwtrash,bell,place, expfood, selectUI,cooking;


    void Update()
    {
        if(GameObject.FindGameObjectWithTag("CookingPan") != null)
        {
            audiocook.enabled = true;
        }
        else
        {
            audiocook.enabled = false;
        }

        if(PlayerPrefs.GetString("sound") == "off")
        {
            audioSrc.volume = 0;
            audiocook.volume = 0;
        }
        else if(PlayerPrefs.GetString("sound") == "on")
        {
            audioSrc.volume = 1;
            audiocook.volume = 1;
        }
    }

    public void PlayPickUp()
    {
        audioSrc.PlayOneShot(pickup);
    }

    public void PlayPlace()
    {
        audioSrc.PlayOneShot(place);
    }

    public void PlayThrow()
    {
        audioSrc.PlayOneShot(throwtrash);
    }

    public void PlayBell()
    {
        audioSrc.PlayOneShot(bell);
    }

    public void PlayExpired()
    {
        audioSrc.PlayOneShot(expfood);
    }
}
