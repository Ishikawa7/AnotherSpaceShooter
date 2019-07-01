using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class Options : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    //for set controls
    private bool isWaitingForKeyPress = false;
    private KeyCode keyPress = KeyCode.None;
    private Button buttonPress;

    [SerializeField]
    private GameObject player1Controls;
    [SerializeField]
    private GameObject player2Controls;

    private Resolution[] resolutions;

    [SerializeField]
    private Dropdown resolutionsDropdown;
    [SerializeField]
    private Dropdown difficultyDropdown;
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private Toggle volumeOnOffToggle;
    [SerializeField]
    private Toggle fullScreenToggle;
    [SerializeField]
    private Dropdown qualityLevelDropdown;

    void Awake()
    {
        SetMenuElementsToCurrentsValues();
    }

    public void FixedUpdate()
    {
        if (isWaitingForKeyPress)
        {
            //polling for detect keypress for set new control
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    keyPress = kcode;
                }
            }
            if(keyPress != KeyCode.None)
            {
                buttonPress.GetComponentInChildren<Text>().text = keyPress.ToString(); //set new button text
                string nomeEtichetta = buttonPress.GetComponentInParent<Text>().name; //use etichette for setting the right controll
                PlayerPrefs.SetString(Prefs.preferences[ nomeEtichetta  ], keyPress.ToString());
                isWaitingForKeyPress = false;
            }
        }
    }
    public void ButtonPress(Button button)
    {
        isWaitingForKeyPress = !isWaitingForKeyPress; //bug fix multiple button click
        buttonPress = button;
        button.GetComponentInChildren<Text>().text = " "; //set text of the button to null during waiting
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
    public void VolumeOnOff(bool on)
    {
        if (on)
        {
            audioMixer.SetFloat("Volume", 0);
        }
        else
        {
            audioMixer.SetFloat("Volume", -80);
        }
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetDifficulty(int difficultyIndex)
    {
        PlayerPrefs.SetInt(Prefs.GameDifficulty, difficultyIndex);
    }
    public void SaveAndExit()
    {
        PlayerPrefs.Save();
        MainMenuManager.Instance.OpenCloseOptionsMenu(false);
        GameData.Load();
        SetMenuElementsToCurrentsValues();
    }
    public void SetMenuElementsToCurrentsValues()
    {
        //set  element to the current value
        #region Set resolution dropDown
        resolutions = Screen.resolutions; //prende l'array con tutte le risoluzioni 
        resolutionsDropdown.ClearOptions(); //pulisce le opzioni del menudropdown

        List<string> options = new List<string>(); //preparo la lista di stringhe delle opzioni
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i; //tengo traccia della risoluzione corrente fra quelle rilevate 
            }
        }

        resolutionsDropdown.AddOptions(options); //aggiungo le opzioni
        resolutionsDropdown.value = currentResolutionIndex; //aggiono il valore da fare vedere di default che sarà quello di default del mio sistema
        resolutionsDropdown.RefreshShownValue();
        #endregion

        #region Set player controls buttons
        //set names of buttons 
        Button[] buttons = player1Controls.GetComponentsInChildren<Button>();
        foreach (Button b in buttons)
        {                                                                       //use button name to get the right control
            b.GetComponentInChildren<Text>().text = GameData.playerControls[0][b.name].ToString();
        }
        buttons = player2Controls.GetComponentsInChildren<Button>();
        foreach (Button b in buttons)
        {
            b.GetComponentInChildren<Text>().text = GameData.playerControls[1][b.name].ToString();
        }

        #endregion

        #region Set volume slider
        float volumeValue;
        audioMixer.GetFloat("Volume", out volumeValue);
        volumeSlider.value = volumeValue;
        #endregion

        #region Set volumeOnOff toggle
        if (volumeValue == -80)
        {
            volumeOnOffToggle.isOn = false;
        }
        else
        {
            volumeOnOffToggle.isOn = true;
        }
        #endregion

        #region Set difficulty dropDown
        difficultyDropdown.value = PlayerPrefs.GetInt(Prefs.GameDifficulty, 0);
        difficultyDropdown.RefreshShownValue();
        #endregion

        #region Set quality dropDown
        qualityLevelDropdown.value = QualitySettings.GetQualityLevel();
        qualityLevelDropdown.RefreshShownValue();
        //set to current full screen
        #endregion

        #region Set fullScren toggle
        if (Screen.fullScreen)
        {
            fullScreenToggle.isOn = true;
        }
        else
        {
            fullScreenToggle.isOn = false;
        }
        #endregion
    }
}
