using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomOutlineColour : MonoBehaviour
{   
    private Tilemap tilemap;
    [SerializeField] LevelColors levelColors;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        // tilemap.color = Random.ColorHSV();

        int randomIndex = Random.Range(0, levelColors.colors.Length);
        tilemap.color = levelColors.colors[randomIndex];
    }

    void Update()
    {
        
    }
}
