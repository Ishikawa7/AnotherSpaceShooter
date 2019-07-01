using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameData
{
    public static Dictionary<string, KeyCode>[] playerControls;
    public static int GameDifficulty { get; set; }
    public static List<int> bestScores;

    static GameData()
    {
        Load();
    }

    public static void Load()
    {
        playerControls = new Dictionary<string, KeyCode>[]
        { //load controls or default control
            new Dictionary<string, KeyCode>()
            { //PLAYER 1 DEFAULT COMMANDS
                { "Shoot", (KeyCode) Enum.Parse( typeof(KeyCode) , PlayerPrefs.GetString(Prefs.P1Shoot,"RightControl") ) },
                { "Right", (KeyCode) Enum.Parse( typeof(KeyCode) , PlayerPrefs.GetString(Prefs.P1Right,"RightArrow") ) },
                { "Left", (KeyCode) Enum.Parse( typeof(KeyCode) , PlayerPrefs.GetString(Prefs.P1Left,"LeftArrow") ) },
                { "Up", (KeyCode) Enum.Parse( typeof(KeyCode) , PlayerPrefs.GetString(Prefs.P1Up,"UpArrow") ) },
                { "Down", (KeyCode) Enum.Parse( typeof(KeyCode) , PlayerPrefs.GetString(Prefs.P1Down,"DownArrow") ) }
            },
            new Dictionary<string, KeyCode>()
            { //PLAYER 2 DEFAULT COMMANDS
                { "Shoot", (KeyCode) Enum.Parse( typeof(KeyCode) , PlayerPrefs.GetString(Prefs.P2Shoot,"Space") ) },
                { "Right", (KeyCode) Enum.Parse( typeof(KeyCode) , PlayerPrefs.GetString(Prefs.P2Right,"D") ) },
                { "Left", (KeyCode) Enum.Parse( typeof(KeyCode) , PlayerPrefs.GetString(Prefs.P2Left,"A") ) },
                { "Up", (KeyCode) Enum.Parse( typeof(KeyCode) , PlayerPrefs.GetString(Prefs.P2Up, "W") ) },
                { "Down", (KeyCode) Enum.Parse( typeof(KeyCode) , PlayerPrefs.GetString(Prefs.P2Down,"S") ) }
            }
        };
        //load gamedifficulty or default
        GameDifficulty = PlayerPrefs.GetInt(Prefs.GameDifficulty, 1);

        //load best scoress
        bestScores = new List<int>
        {
            PlayerPrefs.GetInt("BestScoreAValue", 0) ,
            PlayerPrefs.GetInt("BestScoreBValue", 0) ,
            PlayerPrefs.GetInt("BestScoreCValue", 0) ,
            PlayerPrefs.GetInt("BestScoreDValue", 0) ,
            PlayerPrefs.GetInt("BestScoreEValue", 0) 
        };
    }

    public static void SaveBestScores()
    {
        bestScores.Sort();
        bestScores.Reverse();
        PlayerPrefs.SetInt("BestScoreAValue", bestScores[0]);
        PlayerPrefs.SetInt("BestScoreBValue", bestScores[1]); 
        PlayerPrefs.SetInt("BestScoreCValue", bestScores[2]); 
        PlayerPrefs.SetInt("BestScoreDValue", bestScores[3]); 
        PlayerPrefs.SetInt("BestScoreEValue", bestScores[4]);
        PlayerPrefs.Save();
    }
    


}
