using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private LevelSpawningParams levelSpawningParams;

    private Vector3 platformEnd;
    private GameObject player;
    private bool hasSpawnedPart = false;
    private Transform levelParent;


    void Start()
    {
        player = GameObject.Find("PlayerV2");
        levelParent = GameObject.Find("Level").transform;
        platformEnd = this.GetComponent<TilemapRenderer>().bounds.max;
    }

    void Update()
    {
        float distance;
        distance = Vector3.Distance(player.transform.position, transform.position);
        if(distance < 10 && !hasSpawnedPart){
            SpawnNewLevelPart();
        }
        if(distance > 30 && hasSpawnedPart){
            Destroy(this.gameObject);
        }
    }

    private void SpawnNewLevelPart(){
        // Debug.Log("spawned new part");
        hasSpawnedPart = true;
        Instantiate(levelSpawningParams.levelparts[Random.Range(0, levelSpawningParams.levelparts.Length)], PlatformSpawnPoint(), Quaternion.identity, levelParent);
    }

    private Vector3 PlatformSpawnPoint(){
        Vector3 nextSpawnPoint;

        Vector3 heightOffset = new Vector3(0, Random.Range(levelSpawningParams.minSpawningHeight, levelSpawningParams.maxSpawningHeight), 0);
        Vector3 distanceOffset = new Vector3(Random.Range(levelSpawningParams.minSpawningDistance, levelSpawningParams.maxSpawningDistance), 0, 0);

        nextSpawnPoint = platformEnd + distanceOffset + heightOffset;
        return nextSpawnPoint;
    }
}
