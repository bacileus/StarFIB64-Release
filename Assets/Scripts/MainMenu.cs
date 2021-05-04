using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
   public AudioMixer audioMixer;

   public void PlayGame()
    {
        SceneManager.LoadScene(2);

    }

    public void Level2()
    {
        SceneManager.LoadScene(3);

    }

    public void QuitGame()
    {
        //Debug.Log("QUIT");
        Application.Quit();
    }

    

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
}
