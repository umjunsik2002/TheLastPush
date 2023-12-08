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
        }

        if (languageCode == "KO")
        {
            startButtonText.text = "시작";
            optionsButtonText.text = "설정";
            creditsButtonText.text = "크레딧";
        }
    }
}
