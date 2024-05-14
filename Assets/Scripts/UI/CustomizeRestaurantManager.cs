using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomizeRestaurantManager : MonoBehaviour
{

    Animator anim;
    public GameObject redrestaurant, bluerestaurant, whiterestaurant;
    string restaurant;

    void Start()
    {
        anim = GetComponent<Animator>();
        PlayerPrefs.GetString("restaurant", restaurant);
    }

    void Update()
    {
        Debug.Log(PlayerPrefs.GetString("restaurant"));
        //Show Frames base on Score
        if (PlayerPrefs.GetInt("HighScore") < 150)
        {
            GetDefaultRestaurant();
            bluerestaurant.SetActive(false);
        }
        if (PlayerPrefs.GetInt("HighScore") < 200)
        {
            whiterestaurant.SetActive(false);
        }

        //Show Status to Frame
        if (PlayerPrefs.GetString("restaurant") == "Default")
        {
            redrestaurant.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            redrestaurant.GetComponent<Image>().color = Color.white;
        }
        if (PlayerPrefs.GetString("restaurant") == "Blue")
        {
            bluerestaurant.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            bluerestaurant.GetComponent<Image>().color = Color.white;
        }
        if (PlayerPrefs.GetString("restaurant") == "White")
        {
            whiterestaurant.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            whiterestaurant.GetComponent<Image>().color = Color.white;
        }
    }


    //Set Restaurant Skin
    public void GetDefaultRestaurant()
    {
        restaurant = "Default";
        PlayerPrefs.SetString("restaurant", restaurant);
    }

    public void GetBlueRestaurant()
    {
        restaurant = "Blue";
        PlayerPrefs.SetString("restaurant", restaurant);
    }

    public void GetWhiteRestaurant()
    {
        restaurant = "White";
        PlayerPrefs.SetString("restaurant", restaurant);
    }


    //UI
    public void ReturnMenu()
    {
        StartCoroutine(StartReturning());
    }

    IEnumerator StartReturning()
    {
        anim.SetTrigger("TransitionAnimation");
        yield return new WaitForSeconds(.8f);
        SceneManager.LoadScene("MainMenu");
    }

}
