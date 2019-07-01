using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tutorialPannels;
    private int nowActive;
 
    public void LoadNextTutorialPannel() //call by buttons: secure
    {
        tutorialPannels[nowActive].SetActive(false);
        tutorialPannels[nowActive + 1].SetActive(true);
        nowActive++;
    }
    //supponendo di tenere traccia del fatto che il giocatore ha già visto il tutorial
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
