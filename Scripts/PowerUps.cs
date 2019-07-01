using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PowerUps : MonoBehaviour
{
    
    private AudioSource powerUpAudio;
    [SerializeField]
    private float speed = 0 , seconds = 0 , bonus = 0;
    [SerializeField]
    private UImanager.PowerUpsID powerUpID;
    [SerializeField]
    private GameObject effect;

    private void Start()
    {
        powerUpAudio = gameObject.GetComponent<AudioSource>();
    }
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") //if we collide with the player
        {
            UImanager.Instance.UpdateScore(20);//bonus if collected
            Player player = other.GetComponent<Player>(); //get the reference of the player
            if (player != null) //if we actually get the reference enable triple shoots
            {
                Instantiate(effect, Camera.main.transform.position, Quaternion.identity);
                switch (powerUpID)
                {
                    case UImanager.PowerUpsID.tripleShots:
                        player.EnableTripleShoots(seconds);
                        break;
                    case UImanager.PowerUpsID.speed:
                        player.EnableSpeedBoost(seconds, bonus);
                        break;
                    case UImanager.PowerUpsID.shield:
                        player.EnableShield();
                        break;
                }
            }
        Destroy(this.gameObject);
        }
    }
}
