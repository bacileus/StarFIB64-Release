using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class transitionsCode : MonoBehaviour
{
    public Animator transitionAnim;
    public string sceneName;
    public Button button1;

    void Start()
    {
        Button btn = button1.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        StartCoroutine(LoadScene());
    }

    /*void Update()
    {
        if ()
        {
            StartCoroutine(LoadScene());
        }
    }*/

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}
