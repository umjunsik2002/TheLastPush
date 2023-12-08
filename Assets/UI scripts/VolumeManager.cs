using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public Slider volumeSlider;
    private static VolumeManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static VolumeManager Instance => instance;

    void Start()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = 8f;
            SetVolume();
        }
        else
        {
            Debug.LogError("VolumeSlider not found in the scene. Make sure it exists and is active.");
        }
    }

    void Update()
    {
        SetVolume();
    }

    void SetVolume()
    {
        if (volumeSlider != null)
        {
            float sliderValue = volumeSlider.value;
            float volume = (1.0f / 16.0f) * sliderValue;
            AudioListener.volume = volume;
        }
    }
}