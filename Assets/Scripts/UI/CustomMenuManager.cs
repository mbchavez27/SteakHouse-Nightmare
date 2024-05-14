using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomMenuManager : MonoBehaviour
{
    Animator anim;

    public GameObject whiteChef, blackChef, redChef;
    string chef;

    void Start()
    {
        anim = GetComponent<Animator>();
        PlayerPrefs.GetString("Chef", chef);
    }

    void Update()
    {
        //Show Frames base on Score
        if(PlayerPrefs.GetInt("HighScore") < 100)
        {
            GetDefaultChef();
            blackChef.SetActive(false);
        }
        if(PlayerPrefs.GetInt("HighScore") < 150)
        {
            redChef.SetActive(false);
        }

        //Show Status to Frame
        if(PlayerPrefs.GetString("Chef") == "Default")
        {
            whiteChef.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            whiteChef.GetComponent<Image>().color = Color.white;
        }
        if (PlayerPrefs.GetString("Chef") == "Black")
        {
            blackChef.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            blackChef.GetComponent<Image>().color = Color.white;
        }
        if (PlayerPrefs.GetString("Chef") == "Red")
        {
            redChef.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            redChef.GetComponent<Image>().color = Color.white;
        }
    }

    //Set Chef Skin
    public void GetDefaultChef()
    {
        chef = "Default";
        PlayerPrefs.SetString("Chef", chef);
    }

    public void GetBlackChef()
    {
        chef = "Black";
        PlayerPrefs.SetString("Chef", chef);
    }

    public void GetRedChef()
    {
        chef = "Red";
        PlayerPrefs.SetString("Chef", chef);
    }

    //UI
    public void ReturnMenu()
    {
        StartCoroutine(ShowReturnMenu());
    }

    IEnumerator ShowReturnMenu()
    {
        anim.SetTrigger("TransitionAnimation");
        yield return new WaitForSeconds(.8f);
        SceneManager.LoadScene("MainMenu");
    }
}
