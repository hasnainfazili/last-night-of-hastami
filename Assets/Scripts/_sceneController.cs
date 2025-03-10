using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _sceneController : MonoBehaviour
{
    public static _sceneController instance;
    void Awake()
    {
        instance = this;
    }
    public void StartLevel()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void StartScene()
    {
        SceneManager.LoadScene("Base Scene");
    }
    public void Quit()
    {
      Application.Quit();   
    }
}
