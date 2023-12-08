using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    public Canvas pauseCanvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            clickPause();
        }
    }

    public void clickPause()
    {
        pauseCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void clickResume()
    {
        pauseCanvas.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
