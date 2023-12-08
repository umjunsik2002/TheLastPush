using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseInvoke : MonoBehaviour
{
    public static PauseInvoke Instance;
    public Canvas pauseCanvas;

    private bool canvasStateChanged = false;

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
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            // Enable the PauseCanvas only if it hasn't been enabled before
            if (pauseCanvas != null && !canvasStateChanged)
            {
                Debug.Log("Enabling PauseCanvas");
                pauseCanvas.enabled = true;
                canvasStateChanged = true;
            }
        }
        else
        {
            // Disable the PauseCanvas only if it hasn't been disabled before
            if (pauseCanvas != null && canvasStateChanged)
            {
                Debug.Log("Disabling PauseCanvas");
                pauseCanvas.enabled = false;
                canvasStateChanged = false;
            }
        }
    }
}