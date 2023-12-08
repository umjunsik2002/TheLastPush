using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILanguageSet : MonoBehaviour
{
    public TMP_Text pausedText;
    public TMP_Text resumeText;
    public TMP_Text volumeText;
    public TMP_Text mainMenuText;

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
            pausedText.text = "Paused";
            resumeText.text = "Resume";
            volumeText.text = "Volume";
            mainMenuText.text = "Main Menu";
        }

        if (languageCode == "KO")
        {
            pausedText.text = "일시 정지";
            resumeText.text = "계속하기";
            volumeText.text = "볼륨";
            mainMenuText.text = "메인 메뉴";
        }
    }
}
