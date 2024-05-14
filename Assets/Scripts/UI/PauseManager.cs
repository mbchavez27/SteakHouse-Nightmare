using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseManager : MonoBehaviour
{

    public GameObject pauseMenu,pauseButton;

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        if(Time.timeScale == 1 && !this.GetComponent<timeLimitManager>().gameOver)
        {
            pauseMenu.SetActive(false);
            pauseButton.SetActive(true);
        }
        else if(Time.timeScale == 0 && !this.GetComponent<timeLimitManager>().gameOver)
        {
            pauseMenu.SetActive(true);
            pauseButton.SetActive(false);
        }
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
