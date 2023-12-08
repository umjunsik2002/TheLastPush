using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private PauseManager pauseManager;
    private LanguageManager languageManager;

    private void Start()
    {
        pauseManager = FindObjectOfType<PauseManager>();
        if (pauseManager == null)
        {
            Debug.LogError("PauseManager not found in the scene. Make sure it exists and is active.");
        }

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
        pauseManager.clickPause();
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
