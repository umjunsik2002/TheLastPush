using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelLanguageSet : MonoBehaviour
{
    public TMP_Text backText;
    private LanguageManager languageManager;

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
            backText.text = "Back";
        }

        if (languageCode == "KO")
        {
            backText.text = "뒤로가기";
        }
    }
}
