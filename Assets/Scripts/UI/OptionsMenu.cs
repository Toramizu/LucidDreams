using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionsMenu : Window
{
    [SerializeField] TMP_Dropdown windowMode;

    private void Start()
    {
        int wm = PlayerPrefs.GetInt("windowMode");
        ChangeWindowMode(wm);
        windowMode.value = wm;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Toggle();
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

    public void OpenLogDir()
    {
        System.Diagnostics.Process.Start(Application.persistentDataPath);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
