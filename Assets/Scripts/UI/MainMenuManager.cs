using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    Animator anim;
    public Text HighScoreText;


    void Start()
    {
        anim = GetComponent<Animator>();
        Time.timeScale = 1;
    }

    void Update()
    {
        HighScoreText.text = "HighScore:" + PlayerPrefs.GetInt("HighScore");
    }

    public void PlayGame()
    {
        StartCoroutine(PlayLoadGame());
    }

    public void CustomMenu()
    {
        StartCoroutine(LoadCustomMenu());
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    IEnumerator PlayLoadGame()
    {
        anim.SetTrigger("TransitionAnimation");
        yield return new WaitForSeconds(.8f);
        SceneManager.LoadScene("MainGame");
    }

    IEnumerator LoadCustomMenu()
    {
        anim.SetTrigger("TransitionAnimation");
        yield return new WaitForSeconds(.8f);
        SceneManager.LoadScene("CustomizeRestaurantorChefmenu");
    }

}
