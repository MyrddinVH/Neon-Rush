using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRotate : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxAngle;
    [SerializeField] private float minAngle;


    void Start()
    {
        speed = Random.Range(0.1f, 1f);
        transform.rotation = Quaternion.Euler(0,0, Random.Range(minAngle,maxAngle));
    }

    void Update()
    {
        float rotation = Mathf.SmoothStep(minAngle, maxAngle, Mathf.PingPong(Time.time * speed,1));
        transform.rotation = Quaternion.Euler(0,0,rotation);
    }
}
