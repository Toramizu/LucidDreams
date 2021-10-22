using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : Window
{
    [SerializeField] Window displayOptions;
    [SerializeField] TMP_Dropdown windowMode;

    [SerializeField] Window soundOptions;
    [SerializeField] TMP_InputField seInput;
    [SerializeField] Slider seSlider;

    bool playTestSounds = false;

    private void Start()
    {
        int wm = PlayerPrefs.GetInt("windowMode", 0);
        ChangeWindowMode(wm);
        windowMode.value = wm;

        float se = PlayerPrefs.GetFloat("soundEffects", 50f);
        seSlider.value = se;
        seInput.text = se.ToString();
        ChangeSoundEffects(se);

        playTestSounds = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Toggle();
    }
    
    public void ToggleDisplay()
    {
        displayOptions.Toggle();
        soundOptions.Close();
    }

    public void ChangeWindowMode(int mode)
    {
        //TODO : Save as PlayerPrefs
        switch (mode)
        {
            case 0:
            default:
                Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
                break;
            case 1:
                Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen);
                break;
            case 2:
                Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
                break;
        }

        PlayerPrefs.SetInt("windowMode", mode);
        PlayerPrefs.Save();
    }

    public void ToggleSound()
    {
        soundOptions.Toggle();
        displayOptions.Close();
    }

    public void ChangedSEInutp()
    {
        float volume;
        if(float.TryParse(seInput.text, out volume))
        {
            seSlider.value = volume;
            ChangeSoundEffects(volume);
        }
    }

    public void ChangedSESlider()
    {
        seInput.text = seSlider.value.ToString();
        ChangeSoundEffects(seSlider.value);
    }

    void ChangeSoundEffects(float volume)
    {
        GameManager.Instance.Sound.Volume = volume;
        PlayerPrefs.SetFloat("soundEffects", volume);

        if(playTestSounds)
            GameManager.Instance.Sound.TestSound();
    }

    public void OpenLogDir()
    {
        System.Diagnostics.Process.Start(Application.persistentDataPath);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
