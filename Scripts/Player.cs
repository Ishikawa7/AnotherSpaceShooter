using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, I_canMove, I_canShoot
{
    private Animator animator;

    [SerializeField]
    GameObject explotion;

    [SerializeField]
    private int lives = 0; //in coop minimum beetween the players lives (feature)

    private bool shieldEnable, tripleShotsEnable;

    [SerializeField]
    private GameObject laserPrefab = null, shield;

    private float canFire = 0.0f;

    [SerializeField]
    private float speed, fireRate;

    private UImanager UImanager;
   
    [SerializeField]
    private GameObject[] damages;
    
    public enum PlayerNumber { Player1 , Player2};
    public PlayerNumber playerN;

    void Start()
    {
        UImanager = GameObject.Find("Canvas").GetComponent<UImanager>();
        UImanager.UpdateLives(lives);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //move
        Move();
        //shoot
        if (Input.GetKey(GameData.playerControls[(int)playerN]["Shoot"]))
        {
            Shoot();
        }
        //animate
        Animate();
    }

    public void Move()
    {
        //user input movement
        int up = Input.GetKey(GameData.playerControls[(int)playerN]["Up"]) ? 1 : 0;
        int down = Input.GetKey(GameData.playerControls[(int)playerN]["Down"]) ? -1 : 0;
        int right = Input.GetKey(GameData.playerControls[(int)playerN]["Right"]) ? 1 : 0;
        int left = Input.GetKey(GameData.playerControls[(int)playerN]["Left"]) ? -1 : 0;

        transform.Translate(new Vector3(right + left, up + down, 0) * speed * Time.deltaTime);

        //clamp position
        Vector3 currentPosition = transform.position;
        currentPosition.y = Mathf.Clamp(currentPosition.y, -7.24f, 7.2f);
        currentPosition.x = Mathf.Clamp(currentPosition.x, -10.16f, 10.12f);
        transform.position = currentPosition;
    }
    public void Shoot()
    {
        if (Time.time > canFire)
        {
            if (tripleShotsEnable)
            {
                //if tripleshoots is true instantiate the extra two shoots
                Instantiate(laserPrefab, transform.position + new Vector3(0.551f, 0, 0), Quaternion.identity);
                Instantiate(laserPrefab, transform.position + new Vector3(-0.551f, 0, 0), Quaternion.identity);
            }
            Instantiate(laserPrefab, transform.position + new Vector3(0, 0.77f, 0), Quaternion.identity);

            canFire = Time.time + fireRate;
        }
    }
    //take damage from collision
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy" || other.tag == "EnemyLaser")
        {
            TakeDamage();
        }
    }
    //take damage
    public void TakeDamage()
    {
        if (shieldEnable)
        {
            this.shieldEnable = false;
            this.shield.SetActive(false);
            UImanager.Instance.PowerUpOnOff(UImanager.PowerUpsID.shield, false, playerN);
            return;
        }
 
        lives = lives - 1;
        UImanager.UpdateLives(lives);
        speed = speed * 0.9f; //malus on velocity
        if (lives <= 0)
        {
            Instantiate(this.explotion, this.transform.position, Quaternion.identity);
            GameManager.Instance.NewGame();
            Destroy(this.gameObject);
            return;
        } 
        if(2 - lives >= 0) damages[2 - lives].SetActive(true);
    }
    //triple shoots power up
    public void EnableTripleShoots(float seconds)
    {
        UImanager.Instance.PowerUpOnOff(UImanager.PowerUpsID.tripleShots, true, playerN);
        this.tripleShotsEnable = true;
        StartCoroutine(TripleShotsCountDown(seconds));
    }
    IEnumerator TripleShotsCountDown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.tripleShotsEnable = false;
        UImanager.Instance.PowerUpOnOff(UImanager.PowerUpsID.tripleShots, false, playerN);
    }
    //speed boost power up
    public void EnableSpeedBoost(float seconds, float bonusSpeed)
    {
        UImanager.Instance.PowerUpOnOff(UImanager.PowerUpsID.speed, true, playerN);
        this.speed = speed * bonusSpeed;
        StartCoroutine(SpeedBoostCountDown(seconds, bonusSpeed));
    }
    IEnumerator SpeedBoostCountDown(float seconds, float bonusSpeed)
    {
        yield return new WaitForSeconds(seconds);
        this.speed = speed / bonusSpeed;
        UImanager.Instance.PowerUpOnOff(UImanager.PowerUpsID.speed, false, playerN);
    }
    //enable shield
    public void EnableShield()
    {
        UImanager.Instance.PowerUpOnOff(UImanager.PowerUpsID.shield, true, playerN);
        this.shieldEnable = true;
        this.shield.SetActive(true);
    }
    //animation
    private void Animate()
    {
        bool right = Input.GetKey(GameData.playerControls[(int)playerN]["Right"]);
        bool left = Input.GetKey(GameData.playerControls[(int)playerN]["Left"]);
        animator.SetBool("TurnRight", right);
        animator.SetBool("TurnLeft", left);
    }
}
