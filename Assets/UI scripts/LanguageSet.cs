using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanguageSet : MonoBehaviour
{
    public TMP_Text startButtonText;
    public TMP_Text optionsButtonText;
    public TMP_Text creditsButtonText;
    public TMP_Text quitButtonText;
    public TMP_Text languageText;

    public LanguageManager languageManager;

    private void Start()
    {
        languageManager = FindObjectOfType<LanguageManager>();
        if (languageManager == null)
        {
            Debug.LogError("LanguageManager not found in the scene. Make sure it exists and is active.");
        }
    }

    private void Update()
    {
        if (languageManager != null)
        {
            UpdateSelectedLanguageUI(languageManager.languageCode);
        }
    }

    private void UpdateSelectedLanguageUI(string languageCode)
    {
        if (languageCode == "EN")
        {
            startButtonText.text = "Start";
            optionsButtonText.text = "Options";
            creditsButtonText.text = "Credits";
            quitButtonText.text = "Quit";
            languageText.text = " Languages:";
        }

        if (languageCode == "KO")
        {
            startButtonText.text = "����";
            optionsButtonText.text = "����";
            creditsButtonText.text = "ũ����";
            quitButtonText.text = "����";
            languageText.text = " ���:";
        }
    }
}
