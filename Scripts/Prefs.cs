using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//custom class for keep track of the prefs 
public static class Prefs
{
    public static Dictionary<string, string> preferences = new Dictionary<string, string>();

    public static string GameDifficulty = "GameDifficulty";

    public static string P1Shoot = "P1Shoot";
    public static string P1Right = "P1Right";
    public static string P1Left = "P1Left";
    public static string P1Up = "P1Up";
    public static string P1Down = "P1Down";

    public static string P2Shoot = "P2Shoot";
    public static string P2Right = "P2Right";
    public static string P2Left = "P2Left";
    public static string P2Up = "P2Up";
    public static string P2Down = "P2Down";

    public static string BestScoreAValue = "BestScoreAValue";
    public static string BestScoreBValue = "BestScoreBValue";
    public static string BestScoreCValue = "BestScoreCValue";
    public static string BestScoreDValue = "BestScoreDValue";
    public static string BestScoreEValue = "BestScoreEValue";

    static Prefs()
    {
        Reflection();
    }

    private static void Reflection() //implicitamente non possono essere presenti chiavi duplicate
    {
        Type type = typeof(Prefs);
        foreach(var v in type.GetFields())
        {
            preferences.Add(v.Name, v.Name);
        }
    }

    public static void DebugPrintPreferences()
    {
        Debug.Log("Lista di tutte le prefs SALVATE: ");

        Debug.Log("GameDifficulty ->" + PlayerPrefs.GetInt(Prefs.GameDifficulty));

        Debug.Log("P1Down ->" + PlayerPrefs.GetString(Prefs.P1Down));
        Debug.Log("P1Left ->" + PlayerPrefs.GetString(Prefs.P1Left));
        Debug.Log("P1Right ->" + PlayerPrefs.GetString(Prefs.P1Right));
        Debug.Log("P1Shoot ->" + PlayerPrefs.GetString(Prefs.P1Shoot));
        Debug.Log("P1Up ->" + PlayerPrefs.GetString(Prefs.P1Up));

        Debug.Log("P2Down ->" + PlayerPrefs.GetString(Prefs.P2Down));
        Debug.Log("P2Left ->" + PlayerPrefs.GetString(Prefs.P2Left));
        Debug.Log("P2Right ->" + PlayerPrefs.GetString(Prefs.P2Right));
        Debug.Log("P2Shoot ->" + PlayerPrefs.GetString(Prefs.P2Shoot));
        Debug.Log("P2Up ->" + PlayerPrefs.GetString(Prefs.P2Up));

        Debug.Log("BestScoreAValue ->" + PlayerPrefs.GetInt(Prefs.BestScoreAValue));
        Debug.Log("BestScoreBValue ->" + PlayerPrefs.GetInt(Prefs.BestScoreBValue));
        Debug.Log("BestScoreCValue ->" + PlayerPrefs.GetInt(Prefs.BestScoreCValue));
        Debug.Log("BestScoreDValue ->" + PlayerPrefs.GetInt(Prefs.BestScoreDValue));
        Debug.Log("BestScoreEValue ->" + PlayerPrefs.GetInt(Prefs.BestScoreEValue));
    }                                          
}
