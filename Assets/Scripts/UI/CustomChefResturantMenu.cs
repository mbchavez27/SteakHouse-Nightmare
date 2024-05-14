using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomChefResturantMenu : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
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

    public void ChefMenu()
    {
        StartCoroutine(StartChef());
    }

    IEnumerator StartChef()
    {
        anim.SetTrigger("TransitionAnimation");
        yield return new WaitForSeconds(.8f);
        SceneManager.LoadScene("CustomizationMenu");
    }

    public void RestaurantMenu()
    {
        StartCoroutine(StartRestaurant());
    }

    IEnumerator StartRestaurant()
    {
        anim.SetTrigger("TransitionAnimation");
        yield return new WaitForSeconds(.8f);
        SceneManager.LoadScene("CustomizeRestaurant");
    }
}
