using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject jumpHint;
    [SerializeField] private GameObject dashHint;
    [SerializeField] private GameObject jumpDashHint;
    [SerializeField] private GameObject midAirHint;
    [SerializeField] private GameObject tutorialEnd;
    [SerializeField] private GameObject player;

    [SerializeField] private Transform jumpRespawnPoint;
    [SerializeField] private Transform dashRespawnPoint;
    [SerializeField] private Transform jumpDashRespawnPoint;

    private bool jumpHintIsActive = false;
    private bool dashHintIsActive = false;
    private bool jumpDashHintIsActive = false;
    private bool midAirHintIsActive = false;
    private bool tutorialEndIsActive = false;

    private bool hasJumped;
    private bool hasDashed;


    void Start()
    {
        PlayerTutorialManager.ShowJumpHintEvent += ShowJumpHint;
        PlayerTutorialManager.ShowDashHintEvent += ShowDashHint;
        PlayerTutorialManager.JumpDeathEvent += JumpDeath;
        PlayerTutorialManager.DashDeathEvent += DashDeath;
        PlayerTutorialManager.JumpDashDeathEvent += JumpDashDeath;
        PlayerTutorialManager.ShowJumpDashHintEvent += ShowJumpDashHint;
        PlayerTutorialManager.ShowMidAirHint += ShowMidAirHint;
        PlayerTutorialManager.ShowTutotialEndEvent += ShowTutorialEnd;
        jumpHint.SetActive(false);
        dashHint.SetActive(false);
        jumpDashHint.SetActive(false);
        midAirHint.SetActive(false);
        tutorialEnd.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space) && jumpHintIsActive){
            jumpHintIsActive = false;
            jumpHint.SetActive(false);
            UnPause();
        }

        if(Input.GetKey(KeyCode.Space) && dashHintIsActive){
            dashHintIsActive = false;
            dashHint.SetActive(false);
            UnPause();
        }

        if(Input.GetKey(KeyCode.Space) && jumpDashHintIsActive){
            jumpDashHintIsActive = false;
            jumpDashHint.SetActive(false);
            UnPause();
        }

        if(Input.GetKey(KeyCode.Space) && midAirHintIsActive){
            midAirHintIsActive = false;
            midAirHint.SetActive(false);
            UnPause();
        }

        if(Input.GetKey(KeyCode.Space) && tutorialEndIsActive){
            UnPause();
            SceneManager.LoadScene("Menu");
        }
    }

    private void OnDisable()
    {
        PlayerTutorialManager.ShowJumpHintEvent -= ShowJumpHint;
        PlayerTutorialManager.ShowDashHintEvent -= ShowDashHint;
        PlayerTutorialManager.JumpDeathEvent -= JumpDeath;
        PlayerTutorialManager.DashDeathEvent -= DashDeath;
        PlayerTutorialManager.JumpDashDeathEvent -= JumpDashDeath;
        PlayerTutorialManager.ShowJumpDashHintEvent -= ShowJumpDashHint;
        PlayerTutorialManager.ShowMidAirHint -= ShowMidAirHint;
        PlayerTutorialManager.ShowTutotialEndEvent -= ShowTutorialEnd;
    }

    private void Pause(){
        Time.timeScale = 0;
    }
    
    private void UnPause(){
        Time.timeScale = 1;
    }
    private void ShowJumpHint(){
        jumpHint.SetActive(true);
        jumpHintIsActive = true;
        Pause();
    }

    private void ShowDashHint(){
        dashHint.SetActive(true);
        dashHintIsActive = true;
        Pause();
    }

    private void ShowJumpDashHint(){
        jumpDashHintIsActive = true;
        jumpDashHint.SetActive(true);
        Pause();
    }

    private void ShowMidAirHint(){
        midAirHintIsActive = true;
        midAirHint.SetActive(true);
        Pause();
    }

    private void ShowTutorialEnd(){
        tutorialEndIsActive = true;
        tutorialEnd.SetActive(true);
        Pause();
    }

    private void JumpDeath(){
        player.transform.position = jumpRespawnPoint.position;
    }

    private void DashDeath(){
        player.transform.position = dashRespawnPoint.position;
    }

    private void JumpDashDeath(){
        player.transform.position = jumpDashRespawnPoint.position;
    }
}
