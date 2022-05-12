using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    [SerializeField] private float startTimeBetweenSpawns;
    [SerializeField] private GameObject echo;

    private float timeBetweenSpawns;
    private Rigidbody2D playerRigidbody;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(playerRigidbody.velocity != new Vector2(0,0)){
            if(timeBetweenSpawns <= 0){
                GameObject echoInstance = Instantiate(echo, transform.position, Quaternion.identity);
                timeBetweenSpawns = startTimeBetweenSpawns;
                Destroy(echoInstance, 8f);
            }
            else{
                timeBetweenSpawns -= Time.deltaTime;
            }
        }
    }
}
