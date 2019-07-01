using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//abstract base class for enemy
public abstract class GeneralEnemy : MonoBehaviour, I_canMove
{
    [SerializeField]
    protected int scoreValue = 10; //default 10
    [SerializeField]
    protected float speed = 3; //default 3

    public abstract void Move();

    private GameObject[] otherEnemies;
    private float minimunDistance = 3.5f;

    public void DontOverlapWithOthersEnemies() //not intersection with other enemies
    {
        otherEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in otherEnemies)
        {
            if (Vector3.Distance(enemy.transform.position, this.transform.position) < minimunDistance)
            {
                transform.position = Vector3.MoveTowards(this.transform.position, enemy.transform.position, 0.5f * speed * Time.deltaTime * -1);
            }
        }
    }
    protected GameObject FindTarghet() //coop mode: avoiding only player1 targhetting
    {
        GameObject[] possibleTarghets = GameObject.FindGameObjectsWithTag("Player");
        return possibleTarghets[(int)Random.Range(0, possibleTarghets.Length)];
    }
}
