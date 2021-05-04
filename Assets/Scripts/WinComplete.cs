
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinComplete : MonoBehaviour
{

    public AudioSource audioS;
    public string sceneName;
    public AudioClip soundShoot;

    public void LoadNextLevel()
    {
        audioS.clip = soundShoot;
        audioS.Play();
        SceneManager.LoadScene(sceneName);
    }
}
