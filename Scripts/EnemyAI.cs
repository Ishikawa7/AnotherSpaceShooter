using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : GeneralEnemy, I_canShoot
{ 
    [SerializeField]
    GameObject explotion;

    [SerializeField]
    private GameObject laserPrefab = null;

    private float canFire = 0.0f;

    [SerializeField]
    private float fireRate;

    //AI
    private GameObject targhet;
    private enum State {Indifferent, Approach, Disengagement, Fight }
    private State currentStatus;
    private float distanceFromTarghet;
    private float targhetCoordinateX, targhetCoordinateY;
    private float myCoordinateX, myCoordinateY;
    private float indifferentDistance, approachDistance, disengagementDistance, fightDistance;

    void Start()
    {
        targhet = FindTarghet(); //targhet acquisition
        StartCoroutine(NextStateFunction()); //next state function every 0,5 sec
        //random value to parameters for different behavior
        indifferentDistance = Random.Range(10, 30);
        disengagementDistance = Random.Range(4, 8);
        fightDistance = Random.Range(disengagementDistance, indifferentDistance);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        speed = 5 + (UImanager.Instance.enlapsedTime/ (6000f + UImanager.Instance.enlapsedTime)) * speed; //difficulty over time
        DontOverlapWithOthersEnemies();
        Move();
    }

    public override void Move()
    { 
        if (targhet == null) return;
        //select movement depending on currentState
        switch (currentStatus)
        {
            case State.Indifferent:
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                break;
            case State.Approach:
                transform.position = Vector3.MoveTowards(transform.position, targhet.transform.position, speed * Time.deltaTime);
                Shoot();
                break;
            case State.Disengagement:
                transform.position = Vector3.MoveTowards(transform.position, targhet.transform.position, -speed * Time.deltaTime);
                Shoot();
                break;
            case State.Fight:
                Shoot();
                Vector3 p = new Vector3(0, 0, 0);
                if (( myCoordinateX- targhetCoordinateX) < 0) //left
                {
                    p += Vector3.right;
                }
                else
                {
                    p += Vector3.left;
                }
                if ((myCoordinateY - targhetCoordinateY) > 0) //up
                {
                    p += Vector3.down;
                }
                transform.Translate(p * speed * 0.5f * Time.deltaTime);
                break;
        }
        //clamp position (x)
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, -10.16f, 10.12f);
        transform.position = currentPosition;
        //reuse enemy
        if (this.transform.position.y < -15.0f)
        {
            this.gameObject.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), 20.0f, 0);
        }
    }
    IEnumerator NextStateFunction()
    {
        while (targhet != null)
        {
            targhetCoordinateX = targhet.transform.position.x;
            myCoordinateX = transform.position.x;
            targhetCoordinateY = targhet.transform.position.y;
            myCoordinateY = transform.position.y;

            distanceFromTarghet = Vector3.Distance(transform.position, targhet.transform.position);
            if (distanceFromTarghet < disengagementDistance ) 
            {
                currentStatus = State.Disengagement;
            }                               
            else if (distanceFromTarghet <= indifferentDistance  
                && distanceFromTarghet >= fightDistance
                && myCoordinateY > targhetCoordinateY)
            {
                currentStatus = State.Approach;
            }                               
            else if(distanceFromTarghet < fightDistance 
                && distanceFromTarghet >= disengagementDistance 
                && myCoordinateY > targhetCoordinateY)
            {
                currentStatus = State.Fight;
            }
            else
            {
                currentStatus = State.Indifferent;
            }
            yield return new WaitForSeconds(0.5f);
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

    public void Shoot()
    {
        if (Time.time > canFire)
        {   //shoot logic
            if (myCoordinateX > targhetCoordinateX-2 && myCoordinateX < targhetCoordinateX+2 )
            {
                Instantiate(laserPrefab, transform.position + new Vector3(0.25f, -1.95f, 0), Quaternion.identity);
                Instantiate(laserPrefab, transform.position + new Vector3(-0.25f, -1.95f, 0), Quaternion.identity);
            }
            canFire = Time.time + fireRate;
        }
    }

}
