using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemyAI : GeneralEnemy
{
    [SerializeField]
    GameObject explotion;

    private GameObject player;
    private void Start()
    {
        player = FindTarghet();
    }
    // Update is called once per frame
    void Update()
    {
        speed = 5 + (UImanager.Instance.enlapsedTime / (6000 + UImanager.Instance.enlapsedTime)) * speed; //difficulty over time
        DontOverlapWithOthersEnemies();
        if(player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 20 && player.transform.position.y < transform.position.y + 1)
            {
                Move(player.transform);
            }
            else
            {
                Move();
            }
        }     
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Laser")
        {
            UImanager.Instance.UpdateScore(scoreValue);
            Instantiate(this.explotion, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public override void Move()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (this.transform.position.y < -15.0f) //reuse enemy if ignored
        {
            this.gameObject.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), 20.0f, 0);
        }
    }
    
    public void Move(Transform targhet) //overload methond
    {
        transform.position = Vector3.MoveTowards(transform.position, targhet.position, speed * Time.deltaTime);
    }

}
