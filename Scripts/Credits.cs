using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public int seconds;
    
    void Start()
    {
        StartCoroutine(CreditsCountDown());
    }
    IEnumerator CreditsCountDown()
    {
        yield return new WaitForSeconds(seconds); //credits shows and return to main menu
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
