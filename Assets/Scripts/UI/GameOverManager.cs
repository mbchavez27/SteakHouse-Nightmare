using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Text scoreText, highscoreText;

    void Update()
    {
        scoreText.text = "Score:" + this.GetComponent<scoreManager>().gamescore;
        highscoreText.text = "HighScore:" + PlayerPrefs.GetInt("HighScore");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
