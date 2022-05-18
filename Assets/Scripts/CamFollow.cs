using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{   
    private GameObject player;

    void Update()
    {
        if(player == null){
            player = GameObject.Find("PlayerV2");
        }
        this.transform.position = player.transform.position;
    }
}
