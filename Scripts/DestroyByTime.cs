using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//utility script
public class DestroyByTime : MonoBehaviour
{
    [SerializeField]
    private float time = 0;
    void Start()
    {
        Destroy(gameObject, time);
    }
}
