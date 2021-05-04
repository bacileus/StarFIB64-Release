using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverUI;
    bool gameover = false;
    bool won = false;

    public Slider slider;

    public AudioSource audioS;
    public AudioClip soundGameover;
    public AudioClip soundWin;

    public GameObject WinUI;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameover)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (slider.value == 0)
        {
            OverSound();
            GameOver();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("level01 - copia");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Win()
    {
        WinUI.SetActive(true);
        //Time.timeScale = 0f;
        //GameIsPaused = true;
    }

    void GameOver()
    {
        gameOverUI.SetActive(true);
        //Time.timeScale = 0f;
        //GameIsPaused = true;
    }

    public void OverSound()
    {
        gameover = true;
        audioS.clip = soundGameover;

        if (!audioS.isPlaying) audioS.Play();
    }


}
