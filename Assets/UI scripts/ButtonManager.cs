using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public LanguageManager languageManager;

    private void Start()
    {
        languageManager = FindObjectOfType<LanguageManager>();
        if (languageManager == null)
        {
            Debug.LogError("LanguageManager not found in the scene. Make sure it exists and is active.");
        }
    }

    public void clickStart()
    {
        SceneManager.LoadScene("Level");
    }

    public void clickOptions()
    {
        Time.timeScale = 0f;
        Debug.Log("Time.timeScale: " + Time.timeScale);
    }

    public void clickQuit()
    {
        Application.Quit();
    }

    public void clickBack()
    {
        SceneManager.LoadScene("Start");
    }

    public void clickEnglish()
    {
        languageManager.languageCode = "EN";
    }

    public void clickKorean()
    {
        languageManager.languageCode = "KO";
    }
}
