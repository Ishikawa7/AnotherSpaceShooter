using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private GameObject[] powerUps;

 
    #region Singleton
    public static SpawnManager Instance { get; private set; }
    private SpawnManager() { }

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    public void StartSpawn()
    {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnPowerUps());
    }
    public void StopSpawn()
    {
        StopAllCoroutines();
    }
    IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            yield return new WaitForSeconds(5 + (GameData.GameDifficulty+1) * 7);
            Instantiate(powerUps[Random.Range(0,3)], new Vector3(Random.Range(-10.0f, 10.0f), 10.0f, 0), Quaternion.identity);
        }
    }
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(5 - GameData.GameDifficulty);
            int n = (int)Random.Range(1,GameData.GameDifficulty + 3); //spawn rate on gamedifficulty
            for (int i = 0; i < n; i++)
            {
                Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(Random.Range(-10.0f, 10.0f), 10.0f, 0), Quaternion.identity);
            }
        }
    }
    public void CleanSceneFromEnemies() //clean scene on death
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies)
        {
            Destroy(e);
        }
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            UImanager.Instance.PowerUpOnOff(UImanager.PowerUpsID.shield, false, p.GetComponent<Player>().playerN); //bug fix
            Destroy(p);
        }
    }
}
