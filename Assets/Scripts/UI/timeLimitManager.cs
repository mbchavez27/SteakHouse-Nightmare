using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timeLimitManager : MonoBehaviour
{
    public float time_ = 120;
    public bool gameOver = false;
    public Text timertext;
    public Text plus_minustimetext;
    public GameObject plusminustimetext;
    public GameObject gameOverMenu;

    void Start()
    {
        plusminustimetext.SetActive(false);
    }

    void Update()
    {
        timertext.text = "Time:" + Mathf.Round(time_).ToString();

        //Decrease time 
        time_ -= 1 * Time.deltaTime;

        if(time_ <= 0)
        {
            time_ = 0;
            Time.timeScale = 0;
            gameOver = true;
        }

        if(gameOver)
        {
            gameOverMenu.SetActive(true);
        }
        else
        {
            gameOverMenu.SetActive(false);
        }
    }

    public void ExpiredFoodOrder()
    {
        time_ -= 10;
        StartCoroutine(ShowMinusTime());
    }

    public IEnumerator ShowPlusTime()
    {
        plusminustimetext.SetActive(true);
        plus_minustimetext.text = "+10";
        yield return new WaitForSeconds(1f);
        plusminustimetext.SetActive(false);
    }

    public IEnumerator ShowMinusTime()
    {
        plusminustimetext.SetActive(true);
        plus_minustimetext.text = "-10";
        yield return new WaitForSeconds(1f);
        plusminustimetext.SetActive(false);
    }
}
