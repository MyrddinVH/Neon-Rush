using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceTraveled : MonoBehaviour
{
    private ScoreManager scoreManager;
    private Vector3 startingPoint;
    private float distance;

    void Start()
    {
        startingPoint = transform.position;
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    void Update()
    {
        distance = Mathf.Round(Mathf.Abs(startingPoint.x - this.gameObject.transform.position.x));
        scoreManager.CompareScores(distance);
    }
}
