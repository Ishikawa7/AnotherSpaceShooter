using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{

    [SerializeField]
    private Sprite[] livesSprites;
    [SerializeField]
    private GameObject[] powerUpsSprites;
    [SerializeField]
    public GameObject[] powerUpsSprites2;

    [SerializeField]
    private Image livesImage;

    public enum PowerUpsID { speed, shield, tripleShots}; //speed 0 shield 1 tripleShots 2
    
    private int score = 0;
    private float scoreTimeBonus = 0;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private Animator pauseMenuAnimator;
    [SerializeField]
    private Text bestScoresText;

    public float enlapsedTime = 0;
    private float startTime;

    #region Singleton
    public static UImanager Instance { get; private set; }
    private UImanager() { }

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    public void Start()
    {
        ResetTime();
    }
    public void FixedUpdate()
    {
        enlapsedTime = Time.time - startTime;
        //tecnicamente gioco a tempo (funzione che da un bonus)
        if (scoreTimeBonus>1 && GameObject.FindGameObjectWithTag("Player") != null)
        {
            UpdateScore(1); 
            scoreTimeBonus = 0;
        }
        if (GameObject.FindGameObjectWithTag("Player") != null) //avoid lamer
        {
            scoreTimeBonus += enlapsedTime / (6000 + enlapsedTime);
        }
    }

    public void UpdateLives(int NumberOfLives)
    {
        if(NumberOfLives < 0)
        {
            livesImage.sprite = livesSprites[0];
        }
        else
        {
            livesImage.sprite = livesSprites[NumberOfLives];
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void ResetScore()
    {
        score = 0;
        scoreText.text = "Score: 0";
    }
    public void MenuOnOff(bool mode)
    {
        menu.SetActive(mode);
    }
    public void PauseMenuOnOff(bool mode) //pause game
    {
        pauseMenuAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        pauseMenu.SetActive(mode);
    }
    public void PowerUpOnOff(PowerUpsID powerUp, bool state, Player.PlayerNumber playerN)
    {
        if(playerN == Player.PlayerNumber.Player1)
        {
            if (powerUpsSprites != null) powerUpsSprites[(int)powerUp].SetActive(state);
        }
        if(playerN == Player.PlayerNumber.Player2)
        {
            if(powerUpsSprites2 != null) powerUpsSprites2[(int)powerUp].SetActive(state);
        }
    }

    public void DisplayBestScores()
    {
        string bestScores = "BEST SCORES: \n";
        for (int i = 0; i < 5 ; i++)
        {
            bestScores = bestScores + (i + 1) + ")" + " " + GameData.bestScores[i] + "\n";
        }
        bestScoresText.text = bestScores;
    }

    public void BestScoreUpdate()
    {
        GameData.bestScores.Add(score);
        GameData.bestScores.Sort();
        GameData.bestScores.Reverse();
    }
    public void ResetTime()
    {
        PowerUpClearSprites(); //bug fix
        startTime = Time.time;
        scoreTimeBonus = 0;
    }

    private void PowerUpClearSprites()
    {
        foreach(GameObject go in powerUpsSprites)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in powerUpsSprites2)
        {
            go.SetActive(false);
        }
    }
}
