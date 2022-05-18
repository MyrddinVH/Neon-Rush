using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private AudioSource music;

    void Start()
    {
        music = GameObject.Find("music").GetComponent<AudioSource>();

        if(Time.timeScale != 1){
            Time.timeScale = 1;
            music.Play();
        }

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(!isPaused){
                PauseGame();
            }
            else{
                ContinueGame();
            }
        }
    }

    private void PauseGame(){
        pauseScreen.SetActive(true);
        music.Pause();
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ContinueGame(){
        pauseScreen.SetActive(false);
        music.Play();
        Time.timeScale = 1;
        isPaused = false;
    }

    public void BackToMainMenu(){
        Time.timeScale = 1;
        Destroy(music.gameObject);
        SceneManager.LoadScene("Menu");
    }
}
