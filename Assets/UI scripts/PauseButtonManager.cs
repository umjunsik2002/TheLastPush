using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtonManager : MonoBehaviour
{
    public void clickResume()
    {
        Time.timeScale = 1f;
    }

    public void mainMenuButton()
    {
        if (SceneManager.GetActiveScene().name == "Start")
        {
            Time.timeScale = 1f;
        }
        else
        {
            SceneManager.LoadScene("Start");
            Time.timeScale = 1f;
        }
    }
}
