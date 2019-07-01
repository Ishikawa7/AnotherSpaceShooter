using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float speed = 0;
    [SerializeField]
    private int isEnemy; //1 not enemy //-1 enemy

    void Update()
    {
        transform.Translate( isEnemy *Vector3.up * speed * Time.deltaTime);
    }
    //no friendly fire between enemies
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
