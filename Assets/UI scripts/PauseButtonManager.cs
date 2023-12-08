using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtonManager : MonoBehaviour
{
    public PauseManager pauseManager;

    public void clickResume()
    {
        pauseManager.clickResume();
    }

    public void mainMenuButton()
    {
        if (SceneManager.GetActiveScene().name == "Start")
        {
            pauseManager.clickResume();
        }
        else
        {
            pauseManager.clickResume();
            SceneManager.LoadScene("Start");
        }
    }
}
