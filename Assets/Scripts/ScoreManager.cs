using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreTXT;
    [SerializeField]private ParticleSystem[] fireworks;

    float previousHighScore;
    private float highScore;
    private static ScoreManager instance;
    private Canvas canvas;

    void Start()
    {
        highScore = 0;
        canvas = GetComponent<Canvas>();

        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
    }

    public void CompareScores(float currentScore){
        if(currentScore > highScore){
            highScore = currentScore;
        }

        if(currentScore == 0){
            if(highScore > previousHighScore){
                previousHighScore = highScore;
                PlayerFireworks();
            }
        }
        scoreTXT.text = highScore.ToString() + "M";
    }

    void Update()
    {
        if(canvas.worldCamera == null){
            canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        }
    }


    private void PlayerFireworks(){
        int count = 0;
        foreach (var item in fireworks)
        {
            fireworks[count].Play();
            count += 1;
        }
    }
}
