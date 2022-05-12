using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "level spawning params")]
public class LevelSpawningParams : ScriptableObject
{
    public float minSpawningDistance;
    public float maxSpawningDistance;

    public float minSpawningHeight;
    public float maxSpawningHeight;

    public GameObject[] levelparts;
}
