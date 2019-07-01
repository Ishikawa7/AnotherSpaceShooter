using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField]
    private GameObject optionsPannel;
    [SerializeField]
    private GameObject mainPannel;

    #region Singleton
    public static MainMenuManager Instance { get; private set; }
    private MainMenuManager() { }

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    //load a general scene
    public void LoadScene (string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    //open close optionmenu
    public void OpenCloseOptionsMenu(bool isInOptions)
    {
        optionsPannel.SetActive(isInOptions);
        mainPannel.SetActive(!isInOptions);
    }

    public void DebugPrintPrefsFromMainMenu()
    {
        Prefs.DebugPrintPreferences();
    }
}
