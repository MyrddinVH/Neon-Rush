using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollow : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("PlayerV2");
        Destroy(this.gameObject, 2f);
    }

    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x, this.transform.position.y, this.transform.position.z);
    }
}
