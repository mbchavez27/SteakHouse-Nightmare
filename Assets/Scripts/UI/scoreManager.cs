using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreManager : MonoBehaviour
{
    public Text scoreText;
    public GameObject plusScore;
    public int gamescore = 0;

    void Start()
    {
        plusScore.SetActive(false);
        PlayerPrefs.GetInt("HighScore", gamescore);
    }

    void Update()
    {
        scoreText.text = "Score:" + gamescore.ToString();

        if (this.GetComponent<timeLimitManager>().gameOver)
        {
            SetHighscore();
        }
    }

    void SetHighscore()
    {
        if(gamescore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", gamescore);
        }
    }

    public IEnumerator ShowScore()
    {
        plusScore.SetActive(true);
        yield return new WaitForSeconds(1f);
        plusScore.SetActive(false);
    }
}
