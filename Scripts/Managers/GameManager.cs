using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    //delegate
    private delegate void NewGameProcedure();
    private NewGameProcedure newGameProcedure;

    public enum Mode { singlePlayerMode, coopMode };
    public Mode myMode;
    
    [SerializeField]
    private Player player, player1, player2;

    #region Singleton
    public static GameManager Instance { get; private set; }
    private GameManager(){}
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        newGameProcedure += UImanager.Instance.BestScoreUpdate;
        newGameProcedure += UImanager.Instance.DisplayBestScores;
        newGameProcedure += GameData.SaveBestScores;
        newGameProcedure += UImanager.Instance.ResetScore;
        newGameProcedure += UImanager.Instance.ResetTime;
        newGameProcedure += SpawnManager.Instance.StopSpawn;
        newGameProcedure += SpawnManager.Instance.CleanSceneFromEnemies;

        NewGame();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) //pause menu
        {
            PauseGameOnOff(true);
        }
    }
    //pause game
    public void PauseGameOnOff(bool pause)
    {
        Time.timeScale = pause? 0 : 1;
        UImanager.Instance.PauseMenuOnOff(pause);
    }
    //new game (set all for begin a new game)
    public void NewGame()
    {
        newGameProcedure();
        UImanager.Instance.MenuOnOff(true);
        WaitForChoices();
    }
    //wait until press space to start new game
    private void WaitForChoices()
    {
        StartCoroutine(CountDown(3));
        StartCoroutine(WaitForKeysPress(KeyCode.Space));
        StartCoroutine(WaitForKeysPress(KeyCode.Escape));
    }
    private IEnumerator WaitForKeysPress(KeyCode button) //wait for user to star the game or return to the menu
    {
        yield return new WaitUntil(() => Input.GetKeyDown(button));
        switch (button)
        {
            case KeyCode.Space:
                StartGame();
                break;
            case KeyCode.Escape:
                ToMainMenu();
                break;
        }
    }
        
    private void StartGame()
    {
        StartCoroutine(CountDown(3));
        UImanager.Instance.MenuOnOff(false);
        if (myMode == Mode.singlePlayerMode)
        {
            Instantiate(player, new Vector3(0, -5.0f, 0), Quaternion.identity);
        }
        if(myMode == Mode.coopMode)
        {
            Instantiate(player1, new Vector3(-10f, -5.0f, 0), Quaternion.identity);
            Instantiate(player2, new Vector3(10f, -5.0f, 0), Quaternion.identity);
        }
        SpawnManager.Instance.StartSpawn();
    }
    //to main menu
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    //count down routine 
    private IEnumerator CountDown(int seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
    }
    //reset game
    public void ResetGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
